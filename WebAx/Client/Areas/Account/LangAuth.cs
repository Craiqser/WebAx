using CraB.Core;

namespace WebAx.Client.Areas.Account
{
	[Localization(LanguageId = "ru-RU", KeyPrefix = "Auth")]
	public static class LangAuthRu
	{
		public const string AccountHave = "Есть аккаунт?";
		public const string AccountNo = "Нет аккаунта?";
		public const string Email = "E-mail";
		public const string Login = "Логин";
		public const string Password = "Пароль";
		public const string PasswordConfirm = "Подтвердите пароль";
		public const string Register = "Регистрация";
		public const string SignIn = "Вход в систему";
		public const string SignInBtn = "Войти";
		public const string SignUp = "Регистрация";
	}

	[Localization(LanguageId = "en-US", KeyPrefix = "Auth")]
	public static class LangAuthEn
	{
		public const string AccountHave = "Have an account?";
		public const string AccountNo = "No account?";
		public const string Login = "Login";
		public const string Password = "Password";
		public const string PasswordConfirm = "Password confirmation";
		public const string Register = "Sign up";
		public const string SignInBtn = "Sign in";
	}
}
