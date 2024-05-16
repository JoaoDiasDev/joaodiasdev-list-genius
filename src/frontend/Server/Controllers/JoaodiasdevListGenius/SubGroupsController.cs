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
    [Route("odata/joaodiasdev_list_genius/SubGroups")]
    public partial class SubGroupsController : ODataController
    {
        private JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context;

        public SubGroupsController(JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> GetSubGroups()
        {
            var items = this.context.SubGroups.AsQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>();
            this.OnSubGroupsRead(ref items);

            return items;
        }

        partial void OnSubGroupsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> items);

        partial void OnSubGroupGet(ref SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/joaodiasdev_list_genius/SubGroups(id={id})")]
        public SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> GetSubGroup(long key)
        {
            var items = this.context.SubGroups.Where(i => i.id == key);
            var result = SingleResult.Create(items);

            OnSubGroupGet(ref result);

            return result;
        }
        partial void OnSubGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnAfterSubGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);

        [HttpDelete("/odata/joaodiasdev_list_genius/SubGroups(id={id})")]
        public IActionResult DeleteSubGroup(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubGroups
                    .Where(i => i.id == key)
                    .Include(i => i.Products)
                    .Include(i => i.SharedProducts)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubGroupDeleted(item);
                this.context.SubGroups.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubGroupDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnAfterSubGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);

        [HttpPut("/odata/joaodiasdev_list_genius/SubGroups(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubGroup(long key, [FromBody]JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubGroups
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubGroupUpdated(item);
                this.context.SubGroups.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubGroups.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Group");
                this.OnAfterSubGroupUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/joaodiasdev_list_genius/SubGroups(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubGroup(long key, [FromBody]Delta<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubGroups
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubGroupUpdated(item);
                this.context.SubGroups.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubGroups.Where(i => i.id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Group");
                this.OnAfterSubGroupUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnAfterSubGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item)
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

                this.OnSubGroupCreated(item);
                this.context.SubGroups.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubGroups.Where(i => i.id == item.id);

                Request.QueryString = Request.QueryString.Add("$expand", "Group");

                this.OnAfterSubGroupCreated(item);

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
