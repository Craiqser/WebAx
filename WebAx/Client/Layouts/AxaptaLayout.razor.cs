using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebAx.Client.Areas.Axapta;

namespace WebAx.Client.Layouts
{
	public class AxaptaLayoutPage : LayoutComponentBase
	{
		[Inject]
		protected DaxState DaxState { get; set; }

		private bool _menuAreaCollapse = true;
		protected string MenuAreaDisplayStyle => _menuAreaCollapse ? "none" : "block";

		protected void AreaIdUpdated(string areaId)
		{
			DaxState.AreaId = areaId;
		}

		protected void MenuAreaToggle()
		{
			_menuAreaCollapse = !_menuAreaCollapse;
		}

		protected void ModuleIdUpdated(string moduleId)
		{
			DaxState.ModuleId = moduleId;
		}

		protected override async Task OnParametersSetAsync()
		{
			await DaxState.LoadAsync().ConfigureAwait(false);
		}
	}
}
