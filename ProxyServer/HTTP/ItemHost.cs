namespace ProxyServer.HTTP
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// парсинг хоста и номерa порта
    /// </summary>
    public class ItemHost : ItemBase
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public ItemHost(string source) 
            : base(source)
        {
            Regex myReg = new Regex(@"^(((?<host>.+?):(?<port>\d+?))|(?<host>.+?))$");
            Match m = myReg.Match(source);
            this.Host = m.Groups["host"].Value;

            int tempPort;
            if (!int.TryParse(m.Groups["port"].Value, out tempPort)) tempPort = 80;
            this.Port = tempPort;
        }
    }
}
