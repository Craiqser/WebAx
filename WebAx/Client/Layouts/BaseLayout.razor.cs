using CraB.Core;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebAx.Client.Areas.Axapta;

namespace WebAx.Client.Layouts
{
	public class BaseLayoutPage : LayoutComponentBase
	{
		[Inject]
		protected DaxState DaxState { get; set; }

		protected static string LG(string key)
		{
			return L.Get(key);
		}

		protected override async Task OnParametersSetAsync()
		{
			await DaxState.LoadAsync();
		}
	}
}
