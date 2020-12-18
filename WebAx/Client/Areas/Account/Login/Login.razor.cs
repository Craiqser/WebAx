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

			if (loginResponseModel.Successful)
			{
				await SessionStorage.SetItemAsync(ConstSession.AreaIdKey, loginResponseModel.AreaId).ConfigureAwait(false);
				await SessionStorage.SetItemAsync(ConstSession.DataAreasKey, loginResponseModel.DataAreas).ConfigureAwait(false);
				await SessionStorage.SetItemAsync(ConstSession.LangIdUIKey, CultureHelper.LangId(loginResponseModel.LangId)).ConfigureAwait(false);
				await SessionStorage.SetItemAsync(ConstSession.UserCodeKey, loginResponseModel.UserCode).ConfigureAwait(false);

				NavigationManager.NavigateTo("/");
			}
			else
			{
				Error = loginResponseModel.ErrorDescr;
			}
		}
	}
}
