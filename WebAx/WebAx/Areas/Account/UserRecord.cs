using CraB.Core;
using CraB.Web.Auth;
using Dapper.Contrib.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAx.Areas.Account
{
	/// <summary>Класс пользователя приложения.</summary>
	[Table("Users")]
	public class UserRecord : IUser
	{
		[Required, DisplayName("Id"), Dapper.Contrib.Extensions.Key]
		public int Id { get; init; }

		[Required, DisplayName("Login"), MinLength(3), MaxLength(20)]
		public string Login { get; init; }

		[DisplayName("Name"), MaxLength(50)]
		public string Name { get; init; }

		[DisplayName("Email")]
		[DataType(DataType.EmailAddress), MaxLength(80)]
		public string Email { get; init; }

		public string PasswordHash { get; init; }
		public string PasswordSalt { get; init; }

		[DisplayName("User Image"), MaxLength(100)]
		public string UserImage { get; init; }

		[DisplayName("Language")]
		[MinLength(2), MaxLength(7), DefaultValue("ru")]
		public string LangId { get; init; }

		[Required, DisplayName("Active"), DefaultValue(1)]
		public DeleteOffActive Active { get; init; }
	}
}
