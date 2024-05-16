using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace JoaoDiasDev.ListGenius.UI.Client.Pages
{
    public partial class ProductsLists
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public joaodiasdev_list_geniusService joaodiasdev_list_geniusService { get; set; }

        protected IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> productsLists;

        protected RadzenDataGrid<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> grid0;
        protected int count;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            await grid0.Reload();
        }

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await joaodiasdev_list_geniusService.GetProductsLists(filter: $@"(contains(name,""{search}"") or contains(description,""{search}"") or contains(external_link,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "User", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                productsLists = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ProductsLists" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddProductsList>("Add ProductsList", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> args)
        {
            await DialogService.OpenAsync<EditProductsList>("Edit ProductsList", new Dictionary<string, object> { {"id", args.Data.id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList productsList)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await joaodiasdev_list_geniusService.DeleteProductsList(id:productsList.id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ProductsList"
                });
            }
        }
    }
}