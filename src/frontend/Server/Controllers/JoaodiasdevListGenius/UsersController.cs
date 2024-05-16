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
    [Route("odata/joaodiasdev_list_genius/Users")]
    public partial class UsersController : ODataController
    {
        private JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context;

        public UsersController(JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> GetUsers()
        {
            var items = this.context.Users.AsQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>();
            this.OnUsersRead(ref items);

            return items;
        }

        partial void OnUsersRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> items);

        partial void OnUserGet(ref SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/joaodiasdev_list_genius/Users(id={id})")]
        public SingleResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> GetUser(long key)
        {
            var items = this.context.Users.Where(i => i.id == key);
            var result = SingleResult.Create(items);

            OnUserGet(ref result);

            return result;
        }
        partial void OnUserDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnAfterUserDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);

        [HttpDelete("/odata/joaodiasdev_list_genius/Users(id={id})")]
        public IActionResult DeleteUser(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Users
                    .Where(i => i.id == key)
                    .Include(i => i.ProductsLists)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUserDeleted(item);
                this.context.Users.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUserDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnAfterUserUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);

        [HttpPut("/odata/joaodiasdev_list_genius/Users(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUser(long key, [FromBody]JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Users
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUserUpdated(item);
                this.context.Users.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Users.Where(i => i.id == key);
                
                this.OnAfterUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/joaodiasdev_list_genius/Users(id={id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUser(long key, [FromBody]Delta<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Users
                    .Where(i => i.id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUserUpdated(item);
                this.context.Users.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Users.Where(i => i.id == key);
                
                this.OnAfterUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnAfterUserCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item)
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

                this.OnUserCreated(item);
                this.context.Users.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Users.Where(i => i.id == item.id);

                

                this.OnAfterUserCreated(item);

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
