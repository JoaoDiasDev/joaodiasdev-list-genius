using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace JoaoDiasDev.ListGenius.Hypermedia.Enricher
{
    public class ProductEnricher : ContentResponseEnricher<ProductVO>
    {
        private readonly object _lock = new object();

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
            ;
        }

        protected override Task EnrichModel(ProductVO content, IUrlHelper urlHelper)
        {
            var path = "api/v1/product";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links
                .Add(
                    new HyperMediaLink()
                    {
                        Action = HttpActionVerb.GET,
                        Href = link,
                        Rel = RelationType.self,
                        Type = ResponseTypeFormat.DefaultGet
                    });
            content.Links
                .Add(
                    new HyperMediaLink()
                    {
                        Action = HttpActionVerb.POST,
                        Href = link,
                        Rel = RelationType.self,
                        Type = ResponseTypeFormat.DefaultPost
                    });
            content.Links
              .Add(
                  new HyperMediaLink()
                  {
                      Action = HttpActionVerb.PATCH,
                      Href = link,
                      Rel = RelationType.self,
                      Type = ResponseTypeFormat.DefaultPatch
                  });
            content.Links
                .Add(
                    new HyperMediaLink()
                    {
                        Action = HttpActionVerb.PUT,
                        Href = link,
                        Rel = RelationType.self,
                        Type = ResponseTypeFormat.DefaultPut
                    });
            content.Links
                .Add(
                    new HyperMediaLink()
                    {
                        Action = HttpActionVerb.DELETE,
                        Href = link,
                        Rel = RelationType.self,
                        Type = "int"
                    });
            return Task.CompletedTask;
        }
    }
}
