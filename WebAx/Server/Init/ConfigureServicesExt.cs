using CraB.Web;
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
			JwtSettings jwtSettings = JwtSettings.FromConfiguration(configuration);

			_ = services.AddSingleton(jwtSettings);
			_ = services.AddAuthentication(JwtSettings.AuthScheme)
				.AddJwtBearer(options => options.TokenValidationParameters = jwtSettings.TokenValidationParameters);

			_ = services.AddSingleton<UserService<User>>();

			return services;
		}
	}
}
