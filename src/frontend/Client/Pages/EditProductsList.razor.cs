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
    public partial class EditProductsList
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

        [Parameter]
        public long id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            productsList = await joaodiasdev_list_geniusService.GetProductsListById(id:id);
        }
        protected bool errorVisible;
        protected JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList productsList;

        protected IEnumerable<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> usersForidUsers;


        protected int usersForidUsersCount;
        protected JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User usersForidUsersValue;
        protected async Task usersForidUsersLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await joaodiasdev_list_geniusService.GetUsers(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(user_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                usersForidUsers = result.Value.AsODataEnumerable();
                usersForidUsersCount = result.Count;

                if (!object.Equals(productsList.id_users, null))
                {
                    var valueResult = await joaodiasdev_list_geniusService.GetUsers(filter: $"id eq {productsList.id_users}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        usersForidUsersValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load User" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await joaodiasdev_list_geniusService.UpdateProductsList(id:id, productsList);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(productsList);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            productsList = await joaodiasdev_list_geniusService.GetProductsListById(id:id);
        }
    }
}