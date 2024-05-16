using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

using JoaoDiasDev.ListGenius.UI.Server.Data;

namespace JoaoDiasDev.ListGenius.UI.Server
{
    public partial class joaodiasdev_list_geniusService
    {
        joaodiasdev_list_geniusContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly joaodiasdev_list_geniusContext context;
        private readonly NavigationManager navigationManager;

        public joaodiasdev_list_geniusService(joaodiasdev_list_geniusContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportGroupsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/groups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/groups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGroupsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/groups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/groups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGroupsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> items);

        public async Task<IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>> GetGroups(Query query = null)
        {
            var items = Context.Groups.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnGroupsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGroupGet(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnGetGroupById(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> items);


        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> GetGroupById(long id)
        {
            var items = Context.Groups
                              .AsNoTracking()
                              .Where(i => i.id == id);

 
            OnGetGroupById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGroupGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnAfterGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> CreateGroup(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group _group)
        {
            OnGroupCreated(_group);

            var existingItem = Context.Groups
                              .Where(i => i.id == _group.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Groups.Add(_group);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(_group).State = EntityState.Detached;
                throw;
            }

            OnAfterGroupCreated(_group);

            return _group;
        }

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> CancelGroupChanges(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnAfterGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> UpdateGroup(long id, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group _group)
        {
            OnGroupUpdated(_group);

            var itemToUpdate = Context.Groups
                              .Where(i => i.id == _group.id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(_group);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGroupUpdated(_group);

            return _group;
        }

        partial void OnGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);
        partial void OnAfterGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> DeleteGroup(long id)
        {
            var itemToDelete = Context.Groups
                              .Where(i => i.id == id)
                              .Include(i => i.Products)
                              .Include(i => i.SharedProducts)
                              .Include(i => i.SubGroups)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGroupDeleted(itemToDelete);


            Context.Groups.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGroupDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> items);

        public async Task<IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>> GetProducts(Query query = null)
        {
            var items = Context.Products.AsQueryable();

            items = items.Include(i => i.Group);
            items = items.Include(i => i.ProductsList);
            items = items.Include(i => i.SubGroup);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductGet(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnGetProductById(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> items);


        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> GetProductById(long id)
        {
            var items = Context.Products
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Group);
            items = items.Include(i => i.ProductsList);
            items = items.Include(i => i.SubGroup);
 
            OnGetProductById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnAfterProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> CreateProduct(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product product)
        {
            OnProductCreated(product);

            var existingItem = Context.Products
                              .Where(i => i.id == product.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Products.Add(product);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(product).State = EntityState.Detached;
                throw;
            }

            OnAfterProductCreated(product);

            return product;
        }

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> CancelProductChanges(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnAfterProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> UpdateProduct(long id, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product product)
        {
            OnProductUpdated(product);

            var itemToUpdate = Context.Products
                              .Where(i => i.id == product.id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(product);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProductUpdated(product);

            return product;
        }

        partial void OnProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);
        partial void OnAfterProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> DeleteProduct(long id)
        {
            var itemToDelete = Context.Products
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductDeleted(itemToDelete);


            Context.Products.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportProductsListsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/productslists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/productslists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductsListsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/productslists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/productslists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductsListsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> items);

        public async Task<IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>> GetProductsLists(Query query = null)
        {
            var items = Context.ProductsLists.AsQueryable();

            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnProductsListsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductsListGet(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnGetProductsListById(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> items);


        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> GetProductsListById(long id)
        {
            var items = Context.ProductsLists
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.User);
 
            OnGetProductsListById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnProductsListGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProductsListCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnAfterProductsListCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> CreateProductsList(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList productslist)
        {
            OnProductsListCreated(productslist);

            var existingItem = Context.ProductsLists
                              .Where(i => i.id == productslist.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ProductsLists.Add(productslist);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(productslist).State = EntityState.Detached;
                throw;
            }

            OnAfterProductsListCreated(productslist);

            return productslist;
        }

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> CancelProductsListChanges(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductsListUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnAfterProductsListUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> UpdateProductsList(long id, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList productslist)
        {
            OnProductsListUpdated(productslist);

            var itemToUpdate = Context.ProductsLists
                              .Where(i => i.id == productslist.id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(productslist);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProductsListUpdated(productslist);

            return productslist;
        }

        partial void OnProductsListDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);
        partial void OnAfterProductsListDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> DeleteProductsList(long id)
        {
            var itemToDelete = Context.ProductsLists
                              .Where(i => i.id == id)
                              .Include(i => i.Products)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductsListDeleted(itemToDelete);


            Context.ProductsLists.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductsListDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSharedProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/sharedproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/sharedproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSharedProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/sharedproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/sharedproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSharedProductsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> items);

        public async Task<IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>> GetSharedProducts(Query query = null)
        {
            var items = Context.SharedProducts.AsQueryable();

            items = items.Include(i => i.Group);
            items = items.Include(i => i.SubGroup);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSharedProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSharedProductGet(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnGetSharedProductById(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> items);


        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> GetSharedProductById(long id)
        {
            var items = Context.SharedProducts
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Group);
            items = items.Include(i => i.SubGroup);
 
            OnGetSharedProductById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSharedProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSharedProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnAfterSharedProductCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> CreateSharedProduct(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct sharedproduct)
        {
            OnSharedProductCreated(sharedproduct);

            var existingItem = Context.SharedProducts
                              .Where(i => i.id == sharedproduct.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SharedProducts.Add(sharedproduct);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sharedproduct).State = EntityState.Detached;
                throw;
            }

            OnAfterSharedProductCreated(sharedproduct);

            return sharedproduct;
        }

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> CancelSharedProductChanges(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSharedProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnAfterSharedProductUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> UpdateSharedProduct(long id, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct sharedproduct)
        {
            OnSharedProductUpdated(sharedproduct);

            var itemToUpdate = Context.SharedProducts
                              .Where(i => i.id == sharedproduct.id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sharedproduct);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSharedProductUpdated(sharedproduct);

            return sharedproduct;
        }

        partial void OnSharedProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);
        partial void OnAfterSharedProductDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> DeleteSharedProduct(long id)
        {
            var itemToDelete = Context.SharedProducts
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSharedProductDeleted(itemToDelete);


            Context.SharedProducts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSharedProductDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubGroupsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/subgroups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/subgroups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubGroupsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/subgroups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/subgroups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubGroupsRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> items);

        public async Task<IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>> GetSubGroups(Query query = null)
        {
            var items = Context.SubGroups.AsQueryable();

            items = items.Include(i => i.Group);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubGroupsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubGroupGet(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnGetSubGroupById(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> items);


        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> GetSubGroupById(long id)
        {
            var items = Context.SubGroups
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Group);
 
            OnGetSubGroupById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubGroupGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnAfterSubGroupCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> CreateSubGroup(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup subgroup)
        {
            OnSubGroupCreated(subgroup);

            var existingItem = Context.SubGroups
                              .Where(i => i.id == subgroup.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubGroups.Add(subgroup);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subgroup).State = EntityState.Detached;
                throw;
            }

            OnAfterSubGroupCreated(subgroup);

            return subgroup;
        }

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> CancelSubGroupChanges(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnAfterSubGroupUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> UpdateSubGroup(long id, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup subgroup)
        {
            OnSubGroupUpdated(subgroup);

            var itemToUpdate = Context.SubGroups
                              .Where(i => i.id == subgroup.id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subgroup);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubGroupUpdated(subgroup);

            return subgroup;
        }

        partial void OnSubGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);
        partial void OnAfterSubGroupDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> DeleteSubGroup(long id)
        {
            var itemToDelete = Context.SubGroups
                              .Where(i => i.id == id)
                              .Include(i => i.Products)
                              .Include(i => i.SharedProducts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubGroupDeleted(itemToDelete);


            Context.SubGroups.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubGroupDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> items);

        public async Task<IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>> GetUsers(Query query = null)
        {
            var items = Context.Users.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserGet(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnGetUserById(ref IQueryable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> items);


        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> GetUserById(long id)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.id == id);

 
            OnGetUserById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUserCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnAfterUserCreated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> CreateUser(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User user)
        {
            OnUserCreated(user);

            var existingItem = Context.Users
                              .Where(i => i.id == user.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(user).State = EntityState.Detached;
                throw;
            }

            OnAfterUserCreated(user);

            return user;
        }

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> CancelUserChanges(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnAfterUserUpdated(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> UpdateUser(long id, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User user)
        {
            OnUserUpdated(user);

            var itemToUpdate = Context.Users
                              .Where(i => i.id == user.id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(user);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUserUpdated(user);

            return user;
        }

        partial void OnUserDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);
        partial void OnAfterUserDeleted(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User item);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> DeleteUser(long id)
        {
            var itemToDelete = Context.Users
                              .Where(i => i.id == id)
                              .Include(i => i.ProductsLists)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(itemToDelete);


            Context.Users.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}