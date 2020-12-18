using CraB.Web;

namespace WebAx.Areas.Account
{
	/// <summary>Ответ сервера на попытку входа пользователя.</summary>
	public class LoginResponseModel : ILoginResponseModel
	{
		public string ErrorKey { get; set; }
		public string TokenAccess { get; set; }
		public string TokenRefresh { get; set; }

		/// <summary>Данные пользователя.</summary>
		public UserData User { get; set; }
	}
}
