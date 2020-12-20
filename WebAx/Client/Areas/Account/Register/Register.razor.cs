using CraB.Web;
using CraB.Web.Auth.Client;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account.Register
{
	public class RegisterPage : ComponentBase
	{
		[Inject] private IAuthNService AuthNService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }

		protected string Error { get; set; }
		protected RegisterRequest RegisterModel { get; set; } = new RegisterRequest();

		protected async Task HandleRegistrationAsync()
		{
			Error = string.Empty;
			RegisterResponse result = await AuthNService.Register(RegisterModel);

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
