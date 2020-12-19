using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace WebAx.Client.Areas.Axapta
{
	[Authorize]
	public class AxaptaPage : ComponentBase
	{
		[CascadingParameter(Name = "DaxStateValue")] protected DaxState DaxState { get; set; }
	}
}
