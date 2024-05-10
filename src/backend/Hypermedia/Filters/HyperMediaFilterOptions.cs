using JoaoDiasDev.ListGenius.Hypermedia.Abstract;

namespace JoaoDiasDev.ListGenius.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
