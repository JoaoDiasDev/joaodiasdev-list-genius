using System.Text;

namespace JoaoDiasDev.ListGenius.Hypermedia
{
    public class HyperMediaLink
    {
        private string _href = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string Href
        {
            get
            {
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(_href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set { _href = value; }
        }

        public string Rel { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
