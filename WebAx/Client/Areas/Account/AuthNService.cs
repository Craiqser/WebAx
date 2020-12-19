using Blazored.SessionStorage;
using CraB.Core;
using CraB.Web;
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

		public async Task<RegisterResponseModel> Register(RegisterRequestModel registerRequestModel)
		{
			return await _httpClient.PostJsonAsync<RegisterResponseModel>(ApiAccount.Register, registerRequestModel).ConfigureAwait(false);
		}

		public async Task<ILoginResponseModel> Login(LoginRequestModel loginRequestModel)
		{
			ILoginResponseModel result = await _httpClient.PostJsonAsync<LoginResponseModel>(ApiAccount.Login, loginRequestModel).ConfigureAwait(false);

			if (result is object && result.Successful)
			{
				await _sessionStorage.SetItemAsync(JwtSettings.AuthTokenKeyName, result.Token).ConfigureAwait(false);
				_authNStateProvider.MarkUserAsAuthenticated(result.Token);
			}

			return result;
		}

		public async Task Logout()
		{
			await _sessionStorage.RemoveItemAsync(JwtSettings.AuthTokenKeyName).ConfigureAwait(false);
			_authNStateProvider.MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}
