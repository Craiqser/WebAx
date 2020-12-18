using System.Collections.Generic;
using WebAx.Areas.Axapta;

namespace WebAx.Areas.Account
{
	public class UserData
	{
		/// <summary>Компания пользователя по-умолчанию.</summary>
		public string AreaId { get; set; }

		/// <summary>Список доступных компаний.</summary>
		public IEnumerable<DataArea> DataAreas { get; set; }

		/// <summary>Язык интерфейса по-умолчанию.</summary>
		public string LangId { get; set; }

		/// <summary>Ключи доступа пользователя.</summary>
		public IEnumerable<string> SecurityKeys { get; set; }
	}
}
