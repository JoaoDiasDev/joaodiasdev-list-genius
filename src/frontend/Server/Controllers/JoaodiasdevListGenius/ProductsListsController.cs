using System;
using System.Net;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JoaoDiasDev.ListGenius.UI.Server.Controllers.joaodiasdev_list_genius
{
    [Route("odata/joaodiasdev_list_genius/ProductsLists")]
    public partial class ProductsListsController : ODataController
    {
        private JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context;

        public ProductsListsController(JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> GetProductsLists()
        {
            var items = this.context.ProductsLists.AsQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>();
            this.OnProductsListsRead(ref items);

            return items;
        }

        partial void OnProductsListsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> items);

        partial void OnProductsListGet(ref SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/joaodiasdev_list_genius/ProductsLists(id={id})")]
        public SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> GetProductsList(long key)
        {
            var items = this.context.ProductsLists.Where(i => i.id == key);
            var result = SingleResult.Create(items);

            OnProductsListGet(ref result);

            return result;
        }
        partial void OnProductsListDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnAfterProductsListDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);

        [HttpDelete("/odata/joaodiasdev_list_genius/ProductsLists(id={id})")]
        public IActionResult DeleteProductsList(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ProductsLists
                    .Where(i => i.id == key)
                    .Include(i => i.Products)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnProductsListDeleted(item);
                this.context.ProductsLists.Remove(item);
                this.context.SaveChanges();
                this.OnAfterProductsListDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnProductsListUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnAfterProductsListUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);

        [HttpPut("/odata/joaodiasdev_list_genius/ProductsLists(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutProductsList(long key, [FromBody]JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ProductsLists
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnProductsListUpdated(item);
                this.context.ProductsLists.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ProductsLists.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "User");
                this.OnAfterProductsListUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/joaodiasdev_list_genius/ProductsLists(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchProductsList(long key, [FromBody]Delta<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ProductsLists
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnProductsListUpdated(item);
                this.context.ProductsLists.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ProductsLists.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "User");
                this.OnAfterProductsListUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnProductsListCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnAfterProductsListCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnProductsListCreated(item);
                this.context.ProductsLists.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ProductsLists.Where(i => i.id == item.id);

                Request.QueryString = Request.QueryString.Add("$expand", "User");

                this.OnAfterProductsListCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
