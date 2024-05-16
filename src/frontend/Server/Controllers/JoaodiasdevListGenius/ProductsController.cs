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
    [Route("odata/joaodiasdev_list_genius/Products")]
    public partial class ProductsController : ODataController
    {
        private JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context;

        public ProductsController(JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> GetProducts()
        {
            var items = this.context.Products.AsQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>();
            this.OnProductsRead(ref items);

            return items;
        }

        partial void OnProductsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> items);

        partial void OnProductGet(ref SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/joaodiasdev_list_genius/Products(id={id})")]
        public SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> GetProduct(long key)
        {
            var items = this.context.Products.Where(i => i.id == key);
            var result = SingleResult.Create(items);

            OnProductGet(ref result);

            return result;
        }
        partial void OnProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnAfterProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);

        [HttpDelete("/odata/joaodiasdev_list_genius/Products(id={id})")]
        public IActionResult DeleteProduct(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Products
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnProductDeleted(item);
                this.context.Products.Remove(item);
                this.context.SaveChanges();
                this.OnAfterProductDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnAfterProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);

        [HttpPut("/odata/joaodiasdev_list_genius/Products(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutProduct(long key, [FromBody]JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Products
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnProductUpdated(item);
                this.context.Products.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Products.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Group,ProductsList,SubGroup");
                this.OnAfterProductUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/joaodiasdev_list_genius/Products(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchProduct(long key, [FromBody]Delta<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Products
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnProductUpdated(item);
                this.context.Products.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Products.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Group,ProductsList,SubGroup");
                this.OnAfterProductUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnAfterProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item)
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

                this.OnProductCreated(item);
                this.context.Products.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Products.Where(i => i.id == item.id);

                Request.QueryString = Request.QueryString.Add("$expand", "Group,ProductsList,SubGroup");

                this.OnAfterProductCreated(item);

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
