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
    [Route("odata/joaodiasdev_list_genius/SharedProducts")]
    public partial class SharedProductsController : ODataController
    {
        private JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context;

        public SharedProductsController(JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> GetSharedProducts()
        {
            var items = this.context.SharedProducts.AsQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>();
            this.OnSharedProductsRead(ref items);

            return items;
        }

        partial void OnSharedProductsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> items);

        partial void OnSharedProductGet(ref SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/joaodiasdev_list_genius/SharedProducts(id={id})")]
        public SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> GetSharedProduct(long key)
        {
            var items = this.context.SharedProducts.Where(i => i.id == key);
            var result = SingleResult.Create(items);

            OnSharedProductGet(ref result);

            return result;
        }
        partial void OnSharedProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnAfterSharedProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);

        [HttpDelete("/odata/joaodiasdev_list_genius/SharedProducts(id={id})")]
        public IActionResult DeleteSharedProduct(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SharedProducts
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSharedProductDeleted(item);
                this.context.SharedProducts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSharedProductDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSharedProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnAfterSharedProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);

        [HttpPut("/odata/joaodiasdev_list_genius/SharedProducts(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSharedProduct(long key, [FromBody]JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SharedProducts
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSharedProductUpdated(item);
                this.context.SharedProducts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SharedProducts.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Group,SubGroup");
                this.OnAfterSharedProductUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/joaodiasdev_list_genius/SharedProducts(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSharedProduct(long key, [FromBody]Delta<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SharedProducts
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSharedProductUpdated(item);
                this.context.SharedProducts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SharedProducts.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Group,SubGroup");
                this.OnAfterSharedProductUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSharedProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnAfterSharedProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item)
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

                this.OnSharedProductCreated(item);
                this.context.SharedProducts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SharedProducts.Where(i => i.id == item.id);

                Request.QueryString = Request.QueryString.Add("$expand", "Group,SubGroup");

                this.OnAfterSharedProductCreated(item);

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
