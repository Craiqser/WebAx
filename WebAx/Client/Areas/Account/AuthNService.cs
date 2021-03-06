﻿using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CraB.Web;
using CraB.Web.Auth;
using CraB.Web.Auth.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAx.Areas.Account;

namespace WebAx.Client.Areas.Account
{
	public class AuthNService : IAuthNService
	{
		private readonly HttpClient _httpClient;
		private readonly AuthNStateProvider _authNStateProvider;
		private readonly ILocalStorageService _localStorage;
		private readonly ISessionStorageService _sessionStorage;

		public AuthNService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
			ILocalStorageService localStorage, ISessionStorageService sessionStorage)
		{
			_httpClient = httpClient;
			_authNStateProvider = (AuthNStateProvider)authenticationStateProvider;
			_localStorage = localStorage;
			_sessionStorage = sessionStorage;
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			LoginResponse result = await _httpClient.PostJsonAsync<LoginResponse>(ApiAccount.Login, loginRequest);

			if (result is not null && result.Successful)
			{
				await _localStorage.SetItemAsync(SessionKeys.LangId, result.UserInfo.LangId);
				await _sessionStorage.SetItemAsync(JwtSettings.UserTokenAccess, result.UserInfo.Token);
				await _sessionStorage.SetItemAsync(SessionKeys.UserData, result.UserInfo);

				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtSettings.AuthScheme, result.UserInfo.Token);
				_authNStateProvider.MarkUserAsAuthenticated(result.UserInfo.Token);
			}

			return result;
		}

		public async Task Logout()
		{
			await _sessionStorage.RemoveItemAsync(JwtSettings.UserTokenAccess);
			await _sessionStorage.RemoveItemAsync(SessionKeys.UserData);

			_authNStateProvider.MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}

		public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
		{
			return await _httpClient.PostJsonAsync<RegisterResponse>(ApiAccount.Register, registerRequest);
		}
	}
}
