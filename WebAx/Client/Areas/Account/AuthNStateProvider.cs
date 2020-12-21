﻿using Blazored.SessionStorage;
using CraB.Core;
using CraB.Web;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account
{
	public class AuthNStateProvider : AuthenticationStateProvider
	{
		private readonly ISessionStorageService _sessionStorage;

		public AuthNStateProvider(ISessionStorageService sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = await _sessionStorage.GetItemAsync<string>(JwtSettings.UserTokenAccess);

			if (token.NullOrWhiteSpace())
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtSettings.ParseClaimsFromJwt(token), JwtSettings.AuthType)));
		}

		public void MarkUserAsAuthenticated(string token)
		{
			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(JwtSettings.ParseClaimsFromJwt(token), JwtSettings.AuthType));
			Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(claimsPrincipal));
			NotifyAuthenticationStateChanged(authState);
		}

		public void MarkUserAsLoggedOut()
		{
			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
			Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(claimsPrincipal));
			NotifyAuthenticationStateChanged(authState);
		}
	}
}
