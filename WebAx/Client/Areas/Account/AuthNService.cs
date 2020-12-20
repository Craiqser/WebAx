using Blazored.SessionStorage;
using CraB.Web;
using CraB.Web.Auth;
using CraB.Web.Auth.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
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

		public async Task<RegisterResponse> Register(RegisterRequest registerRequestModel)
		{
			return await _httpClient.PostJsonAsync<RegisterResponse>(ApiAccount.Register, registerRequestModel);
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			LoginResponse result = await _httpClient.PostJsonAsync<LoginResponse>(ApiAccount.Login, loginRequest);

			if (result is object && result.Successful)
			{
				await _sessionStorage.SetItemAsync(JwtSettings.AuthTokenKeyName, result.UserTokens.TokenAccess);
				_authNStateProvider.MarkUserAsAuthenticated(result.UserTokens.TokenAccess);
			}

			return result;
		}

		public async Task Logout()
		{
			await _sessionStorage.RemoveItemAsync(JwtSettings.AuthTokenKeyName);
			_authNStateProvider.MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}
