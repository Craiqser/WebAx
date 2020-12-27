using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CraB.Core;
using CraB.Web.Auth.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAx.Areas.Account;
using WebAx.Client.Areas.Account;
using WebAx.Client.Areas.Axapta;

namespace WebAx.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			_ = builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			_ = builder.Services.AddBlazoredLocalStorage(); // Подключает библиотеку для работы с локальным хранилищем.
			_ = builder.Services.AddBlazoredSessionStorage(); // Подключает библиотеку для работы с хранилищем сессии.
			_ = builder.Services.AddSingleton<ILocalizationService, LocalizationService>(); // Сервис локализации.
			_ = builder.Services.AddScoped<DaxState>();

			// Настройка аутентификации.
			_ = builder.Services.AddAuthorizationCore();
			_ = builder.Services.AddScoped<AuthenticationStateProvider, AuthNStateProvider>();
			_ = builder.Services.AddScoped<IAuthNService, AuthNService>();

			WebAssemblyHost host = builder.Build();

			// Настройка.
			Dependencies.ServiceProviderSet(host.Services); // Установка локатора зависимостей.
			Project.AssemblyAdd(typeof(ApiAccount).Assembly); // Настройка сканируемых на атрибуты сборок.
			_ = LocalizationRegistrator.LocalizationService; // Настройка локализации.

			await host.RunAsync();
		}
	}
}
