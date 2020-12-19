using CraB.Web;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account.Register
{
	public class RegisterPage : ComponentBase
	{
		[Inject] private IAuthNService AuthNService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }

		protected string Error { get; set; }
		protected RegisterRequestModel RegisterModel { get; set; } = new RegisterRequestModel();

		protected async Task HandleRegistrationAsync()
		{
			Error = string.Empty;
			RegisterResponseModel result = await AuthNService.Register(RegisterModel);

			if (result.Successful)
			{
				NavigationManager.NavigateTo("Account/Login");
			}
			else
			{
				Error = result.Error;
			}
		}
	}
}
