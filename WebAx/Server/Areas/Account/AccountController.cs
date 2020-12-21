using CraB.Core;
using CraB.Sql;
using CraB.Web;
using CraB.Web.Auth;
using CraB.Web.Auth.Server;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAx.Areas.Account;

namespace WebAx.Server.Areas.Account
{
	[ApiController]
	public class AccountController : ControllerBase
	{
		/// <summary>Вход пользователя в систему.</summary>
		/// <param name="loginRequest">Модель данных для входа пользователя в систему.</param>
		/// <returns><see cref="LoginResponse" /></returns>
		[HttpPost(ApiAccount.Login)]
		public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
		{
			UserService<User> userService = Dependencies.Resolve<UserService<User>>();
			LoginResponse loginResponse = await userService.LoginAsync(loginRequest);

			/*
			User user = userService.Get(loginRequest?.Login);
			DaxSettings daxSettings = Config.Get<DaxSettings>();

			// Dax.
			IEnumerable<DataArea> dataAreas = CacheApp.Value(CachePrefix.DataAreas, TimeSpan.Zero, () =>
			{
				string sql = @"
select t.ID, t.[NAME], c.[NAME] as NAMEFULL, c.CURRENCYCODE, c.LANGUAGEID
from dbo.DATAAREA as t with(nolock)
	inner join dbo.COMPANYINFO as c with(nolock)
		on (c.DATAAREAID = t.ID)
where (t.ISVIRTUAL = 0);";
				return Query.Select<DataArea>(sql);
			});
			*/

			return loginResponse;
		}

		/// <summary>Регистрирует пользователя в базе данных.</summary>
		/// <param name="registerRequest">Модель данных для регистрации пользователя.</param>
		/// <returns><see cref="RegisterResponse" /></returns>
		[HttpPost(ApiAccount.Register)]
		public async Task<RegisterResponse> Register([FromBody] RegisterRequest registerRequest)
		{
			UserService<User> userService = Dependencies.Resolve<UserService<User>>();
			RegisterResponse registerResponseModel = await userService.RegisterAsync(registerRequest).ConfigureAwait(false);

			if ((registerRequest != null) && registerResponseModel.Successful)
			{
				// Tuple<string, string> pass = Generator.ComputeSHA512(registerRequest.Password.Trim());
				DaxSettings daxSettings = Config.Get<DaxSettings>();

				User user = new User
				{
					Login = registerRequest.Login.Trim(),
					Name = registerRequest.Login.Trim(),
					Email = registerRequest.Email.Length > 0 ? registerRequest.Email.Trim() : null,
					// PasswordHash = pass.Item1,
					// PasswordSalt = pass.Item2,
					// Active = DeleteOffActive.Active,
					LangId = daxSettings.UserLanguageIdDef
				};

				if (await Query.InsertAsync(user).ConfigureAwait(false) > 0)
				{
					return registerResponseModel;
				}
				else
				{
					registerResponseModel.Successful = false;
					registerResponseModel.Error = "User.Auth.RegistrationError";
				}
			}

			return registerResponseModel;
		}

		/// <summary>Обновление токена доступа.</summary>
		/// <param name="loginRequest">Модель данных для входа пользователя в систему, в которой теперь токены.</param>
		/// <returns><see cref="LoginResponse" /></returns>
		[HttpPost(ApiAccount.Token)]
		public async Task<LoginResponse> Token([FromBody] LoginRequest loginRequest)
		{
			UserService<User> userService = Dependencies.Resolve<UserService<User>>();
			LoginResponse loginResponse = await userService.TokenAsync(loginRequest);
			return loginResponse;
		}
	}
}
