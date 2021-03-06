﻿using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CraB.Core;
using CraB.Web.Auth;
using CraB.Web.Auth.Client;
using CraB.Web.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account.Login
{
	public class LoginPage : ComponentBase
	{
		[Inject] private IAuthNService AuthNService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }

		protected string Error { get; set; } = "";
		protected LoginRequest LoginRequest { get; set; } = new LoginRequest();

		protected async Task HandleLoginAsync()
		{
			Error = "";
			LoginResponse loginResponse = await AuthNService.Login(LoginRequest);

			if (loginResponse.Successful)
			{
				NavigationManager.NavigateTo(NavigationManager.QueryString("returnUrl") ?? "/");
			}
			else
			{
				Error = loginResponse.ErrorKey;
			}
		}

		protected static string LG(string key)
		{
			return L.Get(key);
		}
	}
}
