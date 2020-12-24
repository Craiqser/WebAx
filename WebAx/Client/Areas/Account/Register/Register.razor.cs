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
		protected RegisterRequest RegisterRequest { get; set; } = new RegisterRequest();

		protected async Task HandleRegisterAsync()
		{
			Error = "";
			RegisterResponse registerResponse = await AuthNService.Register(RegisterRequest);

			if (registerResponse.Successful)
			{
				NavigationManager.NavigateTo("Account/Login");
			}
			else
			{
				Error = registerResponse.Error;
			}
		}
	}
}
