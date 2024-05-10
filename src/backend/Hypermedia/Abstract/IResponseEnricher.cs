using Microsoft.AspNetCore.Mvc.Filters;

namespace JoaoDiasDev.ListGenius.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
