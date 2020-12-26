using CraB.Core;

namespace WebAx.Client.Areas.Account
{
	[Localization(LanguageId = "ru-RU", KeyPrefix = "Auth")]
	public static class LangAuthRu
	{
		public const string Login = "Логин";
		public const string Password = "Пароль";
		public const string SignIn = "Вход в систему";
	}

	[Localization(LanguageId = "en-US", KeyPrefix = "Auth")]
	public static class LangAuthEn
	{
		public const string Login = "Login";
		public const string Password = "Password";
		public const string SignIn = "Sign In";
	}
}
