using CraB.Web.Auth.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAx.Server.Areas.Account;

namespace WebAx.Server
{
	public static class ConfigureServicesExt
	{
		/// <summary>Добавляет в приложение сервисы данного проекта.</summary>
		public static IServiceCollection WebAxConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			_ = services.AddSingleton<UserService<User>>();

			return services;
		}
	}
}
