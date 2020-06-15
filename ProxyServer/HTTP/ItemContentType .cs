namespace ProxyServer.HTTP
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// анализ полученного от удаленного сервера содержимого и его модификации
    /// </summary>
    public class ItemContentType : ItemBase
    {

        public string Value { get; set; }
        public string Charset { get; set; }

        public ItemContentType(string source) 
            : base(source)
        {
            if (String.IsNullOrEmpty(source)) return;
            // ищем в источнике первое вхождение точки с запятой
            int typeTail = source.IndexOf(";");
            if (typeTail == -1)
            { // все содержимое источника является информацией о типа
                this.Value = source.Trim().ToLower();
                return; // других параметров нет, выходим
            }
            this.Value = source.Substring(0, typeTail).Trim().ToLower();
            // парсим параметры
            string p = source.Substring(typeTail + 1, source.Length - typeTail - 1);
            Regex myReg = new Regex(@"(?<key>.+?)=((""(?<value>.+?)"")|((?<value>[^\;]+)))[\;]{0,1}", RegexOptions.Singleline);
            MatchCollection mc = myReg.Matches(p);
            foreach (Match m in mc)
            {
                if (m.Groups["key"].Value.Trim().ToLower() == "charset")
                {
                    this.Charset = m.Groups["value"].Value;
                }
            }
        }
    }
}
