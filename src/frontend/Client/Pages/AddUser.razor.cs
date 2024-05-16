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
    public partial class AddUser
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

        protected override async Task OnInitializedAsync()
        {
            user = new JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User();
        }
        protected bool errorVisible;
        protected JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User user;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await joaodiasdev_list_geniusService.CreateUser(user);
                DialogService.Close(user);
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
    }
}