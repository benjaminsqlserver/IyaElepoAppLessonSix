﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using IyaElepoApp.Models.ConData;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IyaElepoApp.Models;

namespace IyaElepoApp.Pages
{
    public partial class EditCustomerComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }

        [Parameter]
        public dynamic CustomerID { get; set; }

        IyaElepoApp.Models.ConData.Customer _customer;
        protected IyaElepoApp.Models.ConData.Customer customer
        {
            get
            {
                return _customer;
            }
            set
            {
                if (!object.Equals(_customer, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "customer", NewValue = value, OldValue = _customer };
                    _customer = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<IyaElepoApp.Models.ConData.CustomerType> _getCustomerTypesResult;
        protected IEnumerable<IyaElepoApp.Models.ConData.CustomerType> getCustomerTypesResult
        {
            get
            {
                return _getCustomerTypesResult;
            }
            set
            {
                if (!object.Equals(_getCustomerTypesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCustomerTypesResult", NewValue = value, OldValue = _getCustomerTypesResult };
                    _getCustomerTypesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<IyaElepoApp.Models.ConData.Gender> _getGendersResult;
        protected IEnumerable<IyaElepoApp.Models.ConData.Gender> getGendersResult
        {
            get
            {
                return _getGendersResult;
            }
            set
            {
                if (!object.Equals(_getGendersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGendersResult", NewValue = value, OldValue = _getGendersResult };
                    _getGendersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _GenderID;
        protected int GenderID
        {
            get
            {
                return _GenderID;
            }
            set
            {
                if (!object.Equals(_GenderID, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "GenderID", NewValue = value, OldValue = _GenderID };
                    _GenderID = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _userPassword;
        protected string userPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                if (!object.Equals(_userPassword, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "userPassword", NewValue = value, OldValue = _userPassword };
                    _userPassword = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _confirmUserPassword;
        protected string confirmUserPassword
        {
            get
            {
                return _confirmUserPassword;
            }
            set
            {
                if (!object.Equals(_confirmUserPassword, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "confirmUserPassword", NewValue = value, OldValue = _confirmUserPassword };
                    _confirmUserPassword = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var conDataGetCustomerByCustomerIdResult = await ConData.GetCustomerByCustomerId(CustomerID);
            customer = conDataGetCustomerByCustomerIdResult;

            var conDataGetCustomerTypesResult = await ConData.GetCustomerTypes();
            getCustomerTypesResult = conDataGetCustomerTypesResult;

            var conDataGetGendersResult = await ConData.GetGenders();
            getGendersResult = conDataGetGendersResult;

            GenderID = 0;

            userPassword = "";

            confirmUserPassword = "";
        }

        protected async System.Threading.Tasks.Task Form0Submit(IyaElepoApp.Models.ConData.Customer args)
        {
            await UpdateCustomerAsync();
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
