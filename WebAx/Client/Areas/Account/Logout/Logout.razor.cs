using CraB.Web.Auth.Client;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account.Logout
{
	public class LogoutPage : ComponentBase
	{
		[Inject] private IAuthNService AuthNService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await AuthNService.Logout().ConfigureAwait(true);
			NavigationManager.NavigateTo("/");
		}
	}
}
