using CraB.Core;

namespace WebAx.Client.Areas
{
	[Localization(LanguageId = "ru-RU", KeyPrefix = "Server")]
	public class LangServerRu
	{
		public const string Error = "Ошибка сервера.";
	}

	[Localization(LanguageId = "en-US", KeyPrefix = "Server")]
	public class LangServerEn
	{
		public const string Error = "Server error.";
	}
}
