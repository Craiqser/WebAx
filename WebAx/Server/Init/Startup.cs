using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace WebAx.Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// Добавляет сервисы в контейнер приложения (https://go.microsoft.com/fwlink/?LinkID=398940).
		public void ConfigureServices(IServiceCollection services)
		{
			_ = services.WebAxConfigureServices(Configuration);

			_ = services.AddControllersWithViews();
			_ = services.AddRazorPages();
			_ = services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			_ = app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				_ = app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				_ = app.UseExceptionHandler("/Error");
				_ = app.UseHsts(); // Значение по-умолчанию: 30 дней (https://aka.ms/aspnetcore-hsts).
			}

			_ = app.WebAxConfigure();

			_ = app.UseHttpsRedirection();
			_ = app.UseBlazorFrameworkFiles();
			_ = app.UseStaticFiles();

			_ = app.UseRouting();

			_ = app.UseAuthentication();
			_ = app.UseAuthorization();

			_ = app.UseEndpoints(endpoints =>
			{
				_ = endpoints.MapRazorPages();
				_ = endpoints.MapControllers();
				_ = endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
