using CraB.Core;

namespace WebAx.Server
{
	/// <summary>Настройки модуля "DAX".</summary>
	[SettingKey("DAX")]
	public class DaxSettings
	{
		/// <summary>Компания по умолчанию (если у пользователя не указано иное).</summary>
		public string UserDataAreaIdDef { get; set; }

		/// <summary>Язык по умолчанию (если у пользователя не указано иное).</summary>
		public string UserLanguageIdDef { get; set; }
	}
}
