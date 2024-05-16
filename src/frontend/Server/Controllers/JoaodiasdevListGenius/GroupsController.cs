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
    [Route("odata/joaodiasdev_list_genius/Groups")]
    public partial class GroupsController : ODataController
    {
        private JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context;

        public GroupsController(JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> GetGroups()
        {
            var items = this.context.Groups.AsQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>();
            this.OnGroupsRead(ref items);

            return items;
        }

        partial void OnGroupsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> items);

        partial void OnGroupGet(ref SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/joaodiasdev_list_genius/Groups(id={id})")]
        public SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> GetGroup(long key)
        {
            var items = this.context.Groups.Where(i => i.id == key);
            var result = SingleResult.Create(items);

            OnGroupGet(ref result);

            return result;
        }
        partial void OnGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnAfterGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);

        [HttpDelete("/odata/joaodiasdev_list_genius/Groups(id={id})")]
        public IActionResult DeleteGroup(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Groups
                    .Where(i => i.id == key)
                    .Include(i => i.Products)
                    .Include(i => i.SharedProducts)
                    .Include(i => i.SubGroups)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnGroupDeleted(item);
                this.context.Groups.Remove(item);
                this.context.SaveChanges();
                this.OnAfterGroupDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnAfterGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);

        [HttpPut("/odata/joaodiasdev_list_genius/Groups(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutGroup(long key, [FromBody]JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Groups
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnGroupUpdated(item);
                this.context.Groups.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Groups.Where(i => i.id == key);
                
                this.OnAfterGroupUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/joaodiasdev_list_genius/Groups(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchGroup(long key, [FromBody]Delta<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Groups
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnGroupUpdated(item);
                this.context.Groups.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Groups.Where(i => i.id == key);
                
                this.OnAfterGroupUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnAfterGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item)
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

                this.OnGroupCreated(item);
                this.context.Groups.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Groups.Where(i => i.id == item.id);

                

                this.OnAfterGroupCreated(item);

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
