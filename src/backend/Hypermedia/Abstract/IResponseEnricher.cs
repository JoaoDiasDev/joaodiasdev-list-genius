using Microsoft.AspNetCore.Mvc.Filters;

namespace JoaoDiasDev.ProductList.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
