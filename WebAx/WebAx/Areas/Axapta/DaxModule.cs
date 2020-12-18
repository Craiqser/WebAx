namespace WebAx.Areas.Axapta
{
	public class DaxModule
	{
		/// <summary>Код модуля (Basic).</summary>
		public string Id { get; set; }

		/// <summary>Текст пункта меню (Основное).</summary>
		public string Label { get; set; }

		/// <summary>Текст пункта меню.</summary>
		public string HelpText { get; set; }

		/// <summary>Пусть к файлу иконки пункта меню (/img/bc-start.png).</summary>
		public string IcoSource { get; set; }
	}
}
