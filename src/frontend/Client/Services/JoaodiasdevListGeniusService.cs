
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace JoaoDiasDev.ListGenius.UI.Client
{
    public partial class joaodiasdev_list_geniusService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public joaodiasdev_list_geniusService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/joaodiasdev_list_genius/");
        }


        public async System.Threading.Tasks.Task ExportGroupsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/groups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/groups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportGroupsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/groups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/groups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetGroups(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>> GetGroups(Query query)
        {
            return await GetGroups(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>> GetGroups(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Groups");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGroups(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>>(response);
        }

        partial void OnCreateGroup(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> CreateGroup(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group _group = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group))
        {
            var uri = new Uri(baseUri, $"Groups");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(_group), Encoding.UTF8, "application/json");

            OnCreateGroup(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>(response);
        }

        partial void OnDeleteGroup(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteGroup(long id = default(long))
        {
            var uri = new Uri(baseUri, $"Groups({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteGroup(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetGroupById(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> GetGroupById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"Groups({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGroupById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>(response);
        }

        partial void OnUpdateGroup(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateGroup(long id = default(long), JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group _group = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group))
        {
            var uri = new Uri(baseUri, $"Groups({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", _group.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(_group), Encoding.UTF8, "application/json");

            OnUpdateGroup(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetProducts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>> GetProducts(Query query)
        {
            return await GetProducts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>> GetProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Products");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>>(response);
        }

        partial void OnCreateProduct(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> CreateProduct(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product product = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product))
        {
            var uri = new Uri(baseUri, $"Products");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            OnCreateProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>(response);
        }

        partial void OnDeleteProduct(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteProduct(long id = default(long))
        {
            var uri = new Uri(baseUri, $"Products({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteProduct(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetProductById(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> GetProductById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"Products({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProductById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>(response);
        }

        partial void OnUpdateProduct(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateProduct(long id = default(long), JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product product = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product))
        {
            var uri = new Uri(baseUri, $"Products({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", product.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            OnUpdateProduct(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportProductsListsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/productslists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/productslists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportProductsListsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/productslists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/productslists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetProductsLists(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>> GetProductsLists(Query query)
        {
            return await GetProductsLists(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>> GetProductsLists(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ProductsLists");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProductsLists(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>>(response);
        }

        partial void OnCreateProductsList(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> CreateProductsList(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList productsList = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList))
        {
            var uri = new Uri(baseUri, $"ProductsLists");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(productsList), Encoding.UTF8, "application/json");

            OnCreateProductsList(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>(response);
        }

        partial void OnDeleteProductsList(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteProductsList(long id = default(long))
        {
            var uri = new Uri(baseUri, $"ProductsLists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteProductsList(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetProductsListById(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> GetProductsListById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"ProductsLists({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProductsListById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>(response);
        }

        partial void OnUpdateProductsList(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateProductsList(long id = default(long), JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList productsList = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList))
        {
            var uri = new Uri(baseUri, $"ProductsLists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", productsList.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(productsList), Encoding.UTF8, "application/json");

            OnUpdateProductsList(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSharedProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/sharedproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/sharedproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSharedProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/sharedproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/sharedproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSharedProducts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>> GetSharedProducts(Query query)
        {
            return await GetSharedProducts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>> GetSharedProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SharedProducts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSharedProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>>(response);
        }

        partial void OnCreateSharedProduct(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> CreateSharedProduct(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct sharedProduct = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct))
        {
            var uri = new Uri(baseUri, $"SharedProducts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(sharedProduct), Encoding.UTF8, "application/json");

            OnCreateSharedProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>(response);
        }

        partial void OnDeleteSharedProduct(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSharedProduct(long id = default(long))
        {
            var uri = new Uri(baseUri, $"SharedProducts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSharedProduct(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSharedProductById(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> GetSharedProductById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"SharedProducts({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSharedProductById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>(response);
        }

        partial void OnUpdateSharedProduct(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSharedProduct(long id = default(long), JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct sharedProduct = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct))
        {
            var uri = new Uri(baseUri, $"SharedProducts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", sharedProduct.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(sharedProduct), Encoding.UTF8, "application/json");

            OnUpdateSharedProduct(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSubGroupsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/subgroups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/subgroups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSubGroupsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/subgroups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/subgroups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSubGroups(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>> GetSubGroups(Query query)
        {
            return await GetSubGroups(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>> GetSubGroups(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SubGroups");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubGroups(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>>(response);
        }

        partial void OnCreateSubGroup(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> CreateSubGroup(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup subGroup = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup))
        {
            var uri = new Uri(baseUri, $"SubGroups");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subGroup), Encoding.UTF8, "application/json");

            OnCreateSubGroup(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>(response);
        }

        partial void OnDeleteSubGroup(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSubGroup(long id = default(long))
        {
            var uri = new Uri(baseUri, $"SubGroups({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSubGroup(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSubGroupById(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> GetSubGroupById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"SubGroups({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSubGroupById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>(response);
        }

        partial void OnUpdateSubGroup(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSubGroup(long id = default(long), JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup subGroup = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup))
        {
            var uri = new Uri(baseUri, $"SubGroups({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", subGroup.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(subGroup), Encoding.UTF8, "application/json");

            OnUpdateSubGroup(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/joaodiasdev_list_genius/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/joaodiasdev_list_genius/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>> GetUsers(Query query)
        {
            return await GetUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>> GetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Users");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>>(response);
        }

        partial void OnCreateUser(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> CreateUser(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User user = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User))
        {
            var uri = new Uri(baseUri, $"Users");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnCreateUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>(response);
        }

        partial void OnDeleteUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUser(long id = default(long))
        {
            var uri = new Uri(baseUri, $"Users({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUserById(HttpRequestMessage requestMessage);

        public async Task<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> GetUserById(string expand = default(string), long id = default(long))
        {
            var uri = new Uri(baseUri, $"Users({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>(response);
        }

        partial void OnUpdateUser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUser(long id = default(long), JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User user = default(JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User))
        {
            var uri = new Uri(baseUri, $"Users({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", user.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnUpdateUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}