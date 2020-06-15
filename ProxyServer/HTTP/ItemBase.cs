namespace ProxyServer.HTTP
{
    public class ItemBase
    {
        public string Source { get; set; }

        public ItemBase(string source)
        {
            this.Source = source;
        }
    }
}
