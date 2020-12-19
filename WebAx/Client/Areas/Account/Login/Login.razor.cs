using Blazored.SessionStorage;
using CraB.Core;
using CraB.Web;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebAx.Areas.Account;

namespace WebAx.Client.Areas.Account.Login
{
	public class LoginPage : ComponentBase
	{
		[Inject] private IAuthNService AuthNService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }
		[Inject] private ISessionStorageService SessionStorage { get; set; }

		protected string Error { get; set; }
		protected LoginRequestModel LoginModel { get; set; } = new LoginRequestModel();

		protected async Task HandleLoginAsync()
		{
			Error = string.Empty;
			LoginResponseModel loginResponseModel = (LoginResponseModel)await AuthNService.Login(LoginModel).ConfigureAwait(true);

			if (loginResponseModel.ErrorKey.Length == 0)
			{
				await SessionStorage.SetItemAsync(SessionKeys.AreaIdKey, loginResponseModel.ErrorKey);
				await SessionStorage.SetItemAsync(SessionKeys.AreaIdKey, CultureHelper.LangId(loginResponseModel.TokenAccess));
				await SessionStorage.SetItemAsync(SessionKeys.UserDataKey, loginResponseModel.UserData.ToString());
				NavigationManager.NavigateTo("/Account");
			}
			else
			{
				Error = loginResponseModel.ErrorKey;
			}
		}
	}
}
