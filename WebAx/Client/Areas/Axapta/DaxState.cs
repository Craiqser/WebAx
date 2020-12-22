using Blazored.SessionStorage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAx.Areas.Axapta;

namespace WebAx.Client.Areas.Axapta
{
	/// <summary>Класс состояния Dax для использования в качестве каскадного параметра.</summary>
	public class DaxState
	{
		private ISessionStorageService SessionStorage { get; }
		private ISyncSessionStorageService SyncSessionStorage { get; }
		private string _langId = string.Empty;

		public DaxState(ISessionStorageService sessionStorage, ISyncSessionStorageService syncSessionStorage)
		{
			SessionStorage = sessionStorage;
			SyncSessionStorage = syncSessionStorage;

			// Временная заглушка (перенести в кеш).
			Modules = new[]
			{
				new DaxModule { Id = "Ledger", Label = "Главная книга", IcoSource = "/img/mods/3484.png" },
				new DaxModule { Id = "RAsset", Label = "Основные средства", IcoSource = "/img/mods/3470.png" },
				new DaxModule { Id = "Bank", Label = "Банк", IcoSource = "/img/mods/3451.png" },
				new DaxModule { Id = "Cust", Label = "Расчеты с клиентами", IcoSource = "/img/mods/3480.png" },
				new DaxModule { Id = "Vend", Label = "Расчеты с поставщиками", IcoSource = "/img/mods/3479.png" },
				new DaxModule { Id = "Invent", Label = "Управление запасами", IcoSource = "/img/mods/3486.png" },
				new DaxModule { Id = "DistrBonus", Label = "Премии клиентам", IcoSource = "/img/mods/10453.png" },
				new DaxModule { Id = "SCM2", Label = "Цепочки поставок", IcoSource = "/img/mods/10608.png" },
				new DaxModule { Id = "HRM", Label = "Управление персоналом", IcoSource = "/img/mods/3461.png" },
				new DaxModule { Id = "Basic", Label = "Основное", IcoSource = "/img/mods/3466.png" },
				new DaxModule { Id = "Administration", Label = "Администрирование", IcoSource = "/img/mods/3467.png" },
				new DaxModule { Id = "AsyncExchange", Label = "Асинхронный обмен", IcoSource = "/img/mods/10441.png" },
				new DaxModule { Id = "BTL", Label = "Маркетинг", IcoSource = "/img/mods/10500.png" }
			};
		}

		public string AreaId
		{
			get => DataArea?.Id ?? string.Empty;
			set
			{
				if ((DataArea == null) || (DataArea.Id != value))
				{
					DataArea = DataAreas?.Where(da => da.Id == value).FirstOrDefault();

					if (DataArea != null)
					{
						SyncSessionStorage.SetItem(SessionKeys.Ax.AreaId, DataArea.Id);
						LangId = DataArea.LanguageId;

						if (Module == null)
						{
							ModuleId = Modules?.First()?.Id;
						}
					}
				}
			}
		}

		public string CurrencyCode => DataArea?.CurrencyCode ?? string.Empty;

		public DataArea DataArea { get; private set; }
		public IEnumerable<DataArea> DataAreas { get; private set; }

		public string LangId
		{
			get => _langId;

			set
			{
				if (_langId != (value ?? string.Empty))
				{
					_langId = value;
					SyncSessionStorage.SetItem(SessionKeys.UserData, _langId);
				}
			}
		}

		public DaxModule Module { get; private set; }

		public string ModuleId
		{
			get => Module?.Id ?? string.Empty;
			set
			{
				if ((Module == null) || (Module.Id != value))
				{
					Module = Modules?.Where(m => m.Id == value).FirstOrDefault();

					if (Module != null)
					{
						// Установка нового значения в компоненте меню (запрос нового меню).
					}
				}
			}
		}

		public string ModuleLabel => Module?.Label ?? string.Empty;
		public string ModuleLabelArea => (Module?.Label == null) ? string.Empty : $"{Module.Label} Область";

		public IEnumerable<DaxModule> Modules { get; private set; }

		public string UserCode { get; private set; } = string.Empty;

		public async Task CompanyChange(string areaId)
		{
			if ((DataArea == null) || (DataArea.Id != areaId))
			{
				DataArea = DataAreas?.Where(da => da.Id == areaId).FirstOrDefault();

				if (DataArea != null)
				{
					await SessionStorage.SetItemAsync(SessionKeys.Ax.AreaId, DataArea.Id);
					LangId = DataArea.LanguageId;
				}
			}
		}

		public async Task LoadAsync()
		{
			// DataAreas = await SessionStorage.GetItemAsync<IEnumerable<DataArea>>(SessionKeys.UserData);
			string areaId = await SessionStorage.GetItemAsync<string>(SessionKeys.Ax.AreaId);

			if ((areaId != null) && (DataAreas != null))
			{
				DataArea = DataAreas?.Where(da => da.Id == areaId).FirstOrDefault();

				if (DataArea != null)
				{
					ModuleId = Modules?.First()?.Id;
				}
			}

			_langId = await SessionStorage.GetItemAsync<string>(SessionKeys.UserData);
			UserCode = await SessionStorage.GetItemAsync<string>(SessionKeys.UserData);
		}
	}
}
