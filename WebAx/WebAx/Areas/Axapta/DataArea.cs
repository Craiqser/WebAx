using CraB.Sql;

namespace WebAx.Areas.Axapta
{
	/// <summary>Dax-компания.</summary>
	[ConnectionKey("Work")]
	public class DataArea
	{
		/// <summary>Код компании.</summary>
		public string Id { get; set; }

		/// <summary>Название компании.</summary>
		public string Name { get; set; }

		/// <summary>Полное название компании.</summary>
		public string NameFull { get; set; }

		/// <summary>Валюта (основная).</summary>
		public string CurrencyCode { get; set; }

		/// <summary>Культура.</summary>
		public string LanguageId { get; set; }
	}
}
