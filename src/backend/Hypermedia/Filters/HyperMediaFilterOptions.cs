using JoaoDiasDev.ProductList.Hypermedia.Abstract;

namespace JoaoDiasDev.ProductList.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
