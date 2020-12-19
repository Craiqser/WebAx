using Blazored.SessionStorage;
using CraB.Core;
using CraB.Web;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account
{
	public class AuthNStateProvider : AuthenticationStateProvider
	{
		public HttpClient HttpClient { get; }
		private readonly ISessionStorageService _sessionStorage;

		public AuthNStateProvider(HttpClient httpClient, ISessionStorageService sessionStorage)
		{
			HttpClient = httpClient;
			_sessionStorage = sessionStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = await _sessionStorage.GetItemAsync<string>(JwtSettings.AuthTokenKeyName).ConfigureAwait(false);

			if (token.NullOrWhiteSpace())
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtSettings.AuthScheme, token);

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
