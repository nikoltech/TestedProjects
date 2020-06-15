namespace ProxyServer.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Parser
    {
        private int _HeadersTail = -1;

        public Parser(byte[] source)
        {
            this.Source = source;
            string sourceString = this.GetSourceAsString();
            string httpInfo = sourceString.Substring(0, sourceString.IndexOf("\r\n"));

            // является строка HTTP-запросом и выделить необходимые данные
            Regex myReg = new Regex(@"(?<method>.+)\s+(?<path>.+)\s+HTTP/(?<version>[\d\.]+)", RegexOptions.Multiline);

            // Помещаем в класс найденную информацию о запросе
            if (myReg.IsMatch(httpInfo))
            {
                Match m = myReg.Match(httpInfo);
                this.Method = m.Groups["method"].Value.ToUpper();
                this.Path = m.Groups["path"].Value;
                this.HTTPVersion = m.Groups["version"].Value;
            }
            // обработка HTTP-ответов
            else
            {
                myReg = new Regex(@"HTTP/(?<version>[\d\.]+)\s+(?<status>\d+)\s*(?<msg>.*)");
                Match m = myReg.Match(httpInfo);
                int tempStatusCode;
                int.TryParse(m.Groups["status"].Value, out tempStatusCode);
                this.StatusCode = tempStatusCode;
                this.StatusMessage = m.Groups["msg"].Value;
                this.HTTPVersion = m.Groups["version"].Value;
            }

            // получить сами заголовки и их значения
            _HeadersTail = sourceString.IndexOf("\r\n\r\n");
            if (_HeadersTail != -1)
            {
                sourceString = sourceString.Substring(sourceString.IndexOf("\r\n") + 2, _HeadersTail - sourceString.IndexOf("\r\n") - 2);
            }

            // заполнить коллекцию HTTP-заголовками
            this.Items = new Dictionary<string, ItemBase>(StringComparer.CurrentCultureIgnoreCase);
            myReg = new Regex(@"^(?<key>[^\x3A]+)\:\s{1}(?<value>.+)$", RegexOptions.Multiline);
            MatchCollection mc = myReg.Matches(sourceString);
            foreach (Match mm in mc)
            {
                string key = mm.Groups["key"].Value;
                if (!this.Items.ContainsKey(key))
                {
                    // если указанного заголовка нет в коллекции, добавляем его
                    // в зависимости от типа
                    if (key.Trim().ToLower() == "host")
                    {
                        this.Items.Add(key, new ItemHost(mm.Groups["value"].Value.Trim("\r\n ".ToCharArray())));
                    }
                    else if (key.Trim().ToLower() == "content-type")
                    {
                        this.Items.Add(key, new ItemContentType(mm.Groups["value"].Value.Trim("\r\n ".ToCharArray())));
                    }
                    else
                    {
                        this.Items.Add(key, new ItemBase(mm.Groups["value"].Value.Trim("\r\n ".ToCharArray())));
                    }
                }
            }
        }

        public string Host
        {
            get
            {
                if (!this.Items.ContainsKey("Host")) return String.Empty;
                return ((ItemHost)this.Items["Host"]).Host;
            }
        }

        public int Port
        {
            get
            {
                if (!this.Items.ContainsKey("Host")) return 80;
                return ((ItemHost)this.Items["Host"]).Port;
            }
        }

        public byte[] Source { get; set; }

        public string HTTPVersion { get; set; }

        public string Method { get; set; }

        public string Path { get; set; }

        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public Dictionary<string, ItemBase> Items { get; set; }

        public string GetSourceAsString()
        {
            return Encoding.ASCII.GetString(this.Source);
        }

        /// <summary>
        /// хелпер-функция будет возвращать содержимое запроса/ответа выполняя декомпресию по мере необходимости
        /// </summary>
        /// <returns></returns>
        public byte[] GetBody()
        {
            // хвоста нет, значит тела тоже нет
            if (_HeadersTail == -1) return null;

            // выделяем тело, начиная с конца хвоста
            byte[] result = new byte[this.Source.Length - _HeadersTail - 4];
            Buffer.BlockCopy(this.Source, _HeadersTail + 4, result, 0, result.Length);

            // тело может быть сжато, проверяем так это или нет
            if (this.Items != null && this.Items.ContainsKey("Content-Encoding") && this.Items["Content-Encoding"].Source.ToLower() == "gzip")
            {
                // если данные сжаты, то разархивируем их
                GZipStream myGzip = new GZipStream(new MemoryStream(result), CompressionMode.Decompress);
                using (MemoryStream m = new MemoryStream())
                {
                    byte[] buffer = new byte[512];
                    int len = 0;
                    while ((len = myGzip.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        m.Write(buffer, 0, len);
                    }
                    result = m.ToArray();
                }
            }

            // возвращаем результат
            return result;
        }

        /// <summary>
        /// преобразовывает полученный массив байт из GetBody в текст
        /// </summary>
        /// <returns></returns>
        public string GetBodyAsString()
        {
            Encoding e = Encoding.UTF8;

            // если есть тип содержимого, то может есть и кодировка
            if (this.Items != null && this.Items.ContainsKey("Content-Type") && !String.IsNullOrEmpty(((ItemContentType)this.Items["Content-Type"]).Charset))
            {
                // кодировка есть, используем её при декодировании содержимого
                try
                {
                    e = Encoding.GetEncoding(((ItemContentType)this.Items["Content-Type"]).Charset);
                }
                catch { }
            }
            return e.GetString(GetBody());
        }

        /// <summary>
        /// Если нам потребуется изменять содержимое, то проще всего это делать с оригинальными данными, 
        /// т.е. теми данными, которые находятся в текущем экземпляре класса Parser. Но следует учитывать, 
        /// что при изменении содержимого может потребоваться, также изменить значения некоторых заголовков, 
        /// главным образом Content-Length, который содержит информацию об объме передаваемых данных. 
        /// Поэтому, функция для изменения содержимого должна не только найти в оригинальных данных текст 
        /// и заменить его, но и обновить значения заголовка Content-Length.
        /// </summary>
        /// <param name="newBody"></param>
        public void SetStringBody(string newBody)
        {
            if (this.StatusCode <= 0)
            {
                throw new Exception("Можно изменять только содержимое, полученное в ответ от удаленного сервера.");
            }
            Encoding e = Encoding.UTF8;
            string result = String.Format("HTTP/{0} {1} {2}", this.HTTPVersion, this.StatusCode, this.StatusMessage);
            foreach (string k in this.Items.Keys)
            {
                ItemBase itm = this.Items[k];
                if (!String.IsNullOrEmpty(result)) result += "\r\n";
                if (k.ToLower() == "content-length")
                {
                    // информация о размере содержимого, меняем
                    result += String.Format("{0}: {1}", k, newBody.Length);
                }
                else if (k.ToLower() == "content-encoding" && itm.Source.ToLower() == "gzip")
                {
                    // если оригинальное содержимое сжато, то пропускаем этот заголовок, т.к. у нас обратного сжатия нет (но можно сделать, если нужно)
                }
                else
                {
                    // другие заголовки оставляем без изменений
                    result += String.Format("{0}: {1}", k, itm.Source);

                    // если это тип содержимого, то смотрим, может есть информация о кодировке
                    if (k.ToLower() == "content-type" && !String.IsNullOrEmpty(((ItemContentType)this.Items["Content-Type"]).Charset))
                    {
                        // кодировка есть, используем её при кодировании содержимого
                        try
                        {
                            e = Encoding.GetEncoding(((ItemContentType)this.Items["Content-Type"]).Charset);
                        }
                        catch { }
                    }
                }
            }

            result += "\r\n\r\n";

            // добавляем тело
            result += newBody;

            // переносим в источник
            this.Source = e.GetBytes(result);
        }
    }
}
