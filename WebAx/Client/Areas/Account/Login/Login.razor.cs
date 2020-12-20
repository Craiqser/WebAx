using Blazored.SessionStorage;
using CraB.Web.Auth;
using CraB.Web.Auth.Client;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account.Login
{
	public class LoginPage : ComponentBase
	{
		[Inject] private IAuthNService AuthNService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }
		[Inject] private ISessionStorageService SessionStorage { get; set; }

		protected string Error { get; set; }
		protected LoginRequest LoginRequest { get; set; } = new LoginRequest();

		protected async Task HandleLoginAsync()
		{
			Error = string.Empty;
			LoginResponse loginResponse = await AuthNService.Login(LoginRequest);

			if (loginResponse.Successful)
			{
				await SessionStorage.SetItemAsync(SessionKeys.AreaIdKey, loginResponse.ErrorKey);
				// await SessionStorage.SetItemAsync(SessionKeys.AreaIdKey, CultureHelper.LangId(loginResponse.UserPayload.TokenAccess));
				// await SessionStorage.SetItemAsync(SessionKeys.UserDataKey, loginResponse.UserPayload.ToString());
				NavigationManager.NavigateTo("/Account");
			}
			else
			{
				Error = loginResponse.ErrorKey;
			}
		}
	}
}
