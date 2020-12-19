using CraB.Core;
using Microsoft.AspNetCore.Builder;
using WebAx.Areas.Account;

namespace WebAx.Server
{
	/// <summary>Настраивает конвейер приложения.</summary>
	public static class ConfigureExt
	{
		public static IApplicationBuilder WebAxConfigure(this IApplicationBuilder app)
		{
			Dependencies.ServiceProviderSet(app?.ApplicationServices); // Установка локатора зависимостей.
			_ = app.ConfigureAssemblies(); // Настройка сканируемых на атрибуты сборок.
			return app;
		}

		private static IApplicationBuilder ConfigureAssemblies(this IApplicationBuilder app)
		{
			// Текущая сборка и сборки библиотеки CraB.* уже загружены. Загружаем общую и клиентскую сборки.
			Project.AssemblyAdd(typeof(ApiAccount).Assembly);
			Project.AssemblyAdd(typeof(SessionKeys).Assembly);
			return app;
		}
	}
}
