using CraB.Web;
using System.Threading.Tasks;

namespace WebAx.Client.Areas.Account
{
	public interface IAuthNService
	{
		/// <summary>Вход пользователя.</summary>
		/// <param name="loginModel"></param>
		Task<ILoginResponseModel> Login(LoginRequestModel loginRequestModel);

		/// <summary>Выход пользователя из системы.</summary>
		Task Logout();

		/// <summary>Регистрация пользователя.</summary>
		/// <param name="registerModel"></param>
		Task<RegisterResponseModel> Register(RegisterRequestModel registerRequestModel);
	}
}
