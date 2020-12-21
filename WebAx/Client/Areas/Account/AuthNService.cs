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
		private readonly ISessionStorageService _sessionStorage;

		public AuthNService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ISessionStorageService sessionStorage)
		{
			_httpClient = httpClient;
			_authNStateProvider = (AuthNStateProvider)authenticationStateProvider;
			_sessionStorage = sessionStorage;
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			LoginResponse result = await _httpClient.PostJsonAsync<LoginResponse>(ApiAccount.Login, loginRequest);

			if (result is not null && result.Successful)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtSettings.AuthScheme, result.UserTokens.TokenAccess);
				await _sessionStorage.SetItemAsync(JwtSettings.UserTokenAccess, result.UserTokens.TokenAccess);
				await _sessionStorage.SetItemAsync(JwtSettings.UserTokenRefresh, result.UserTokens.TokenRefresh);
				_authNStateProvider.MarkUserAsAuthenticated(result.UserTokens.TokenAccess);
			}

			return result;
		}

		public async Task Logout()
		{
			await _sessionStorage.RemoveItemAsync(JwtSettings.UserTokenAccess);
			await _sessionStorage.RemoveItemAsync(JwtSettings.UserTokenRefresh);
			_authNStateProvider.MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}

		public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
		{
			return await _httpClient.PostJsonAsync<RegisterResponse>(ApiAccount.Register, registerRequest);
		}

		public async Task TokenRefresh()
		{
			string token = await _sessionStorage.GetItemAsync<string>(JwtSettings.UserTokenAccess);
			string refreshToken = await _sessionStorage.GetItemAsync<string>(JwtSettings.UserTokenRefresh);

			LoginResponse tokenResponse = await _httpClient.PostJsonAsync<LoginResponse>(ApiAccount.Token,
				new LoginRequest { Login = token, Password = refreshToken });

			if (tokenResponse.Successful)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtSettings.AuthScheme, tokenResponse.UserTokens.TokenAccess);
				await _sessionStorage.SetItemAsync(JwtSettings.UserTokenAccess, tokenResponse.UserTokens.TokenAccess);
				await _sessionStorage.SetItemAsync(JwtSettings.UserTokenRefresh, tokenResponse.UserTokens.TokenRefresh);
			}
		}
	}
}
