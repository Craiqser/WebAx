using CraB.Core;
using CraB.Sql;
using CraB.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAx.Areas.Account;
using WebAx.Areas.Axapta;

namespace WebAx.Server.Areas.Account
{
	[ApiController]
	public class AccountController : ControllerBase
	{
		/// <summary>Вход пользователя в систему.</summary>
		/// <param name="loginRequestModel">Модель данных для входа пользователя в систему.</param>
		/// <returns><see cref="LoginResponseModel" /></returns>
		[HttpPost(ApiAccount.Login)]
		public async Task<LoginResponseModel> Login([FromBody] LoginRequestModel loginRequestModel)
		{
			UserService<User> userService = Dependencies.Resolve<UserService<User>>();
			LoginResponseModel loginResponseModel = await userService.LoginAsync<LoginResponseModel>(loginRequestModel).ConfigureAwait(false);
			User user = userService.Get(loginRequestModel?.UserName);
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

			UserData userData = new UserData
			{
				AreaId = user.DataAreaDefault.NullOrEmpty() ? daxSettings.UserDataAreaIdDef : user.DataAreaDefault,
				LangId = user.LanguageId.NullOrEmpty() ? daxSettings.UserLanguageIdDef : user.LanguageId,
				DataAreas = null,
				SecurityKeys = null
			};

			loginResponseModel.UserData = userData;
			return loginResponseModel;
		}

		/// <summary>Регистрирует пользователя в базе данных.</summary>
		/// <param name="registerRequestModel">Модель данных для регистрации пользователя.</param>
		/// <returns><see cref="RegisterResponseModel" /></returns>
		[HttpPost(ApiAccount.Register)]
		public async Task<RegisterResponseModel> Register([FromBody] RegisterRequestModel registerRequestModel)
		{
			UserService<User> userService = Dependencies.Resolve<UserService<User>>();
			RegisterResponseModel registerResponseModel = await userService.RegisterAsync(registerRequestModel).ConfigureAwait(false);

			if ((registerRequestModel != null) && registerResponseModel.Successful)
			{
				Tuple<string, string> pass = Generator.ComputeSHA512(registerRequestModel.Password.Trim());
				DaxSettings daxSettings = Config.Get<DaxSettings>();

				User user = new User
				{
					Login = registerRequestModel.UserName.Trim(),
					DisplayName = registerRequestModel.UserName.Trim(),
					Email = registerRequestModel.Email.Length > 0 ? registerRequestModel.Email.Trim() : null,
					PasswordHash = pass.Item1,
					PasswordSalt = pass.Item2,
					Active = DeleteOffActive.Active,
					Code = Generator.StringFromGuid(5),
					DataAreaDefault = daxSettings.UserDataAreaIdDef,
					LanguageId = daxSettings.UserLanguageIdDef
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
	}
}
