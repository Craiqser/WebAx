using CraB.Core;
using CraB.Web;
using Dapper.Contrib.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAx.Server.Areas.Account
{
	/// <summary>Класс пользователя приложения.</summary>
	[Table("Users")]
	public class User : IAuthUser
	{
		[Required, DisplayName("Id"), Dapper.Contrib.Extensions.Key]
		public int Id { get; set; }

		[Required, DisplayName("Login"), MinLength(3), MaxLength(20)]
		public string Login { get; set; }

		[DisplayName("Display Name"), MaxLength(50)]
		public string DisplayName { get; set; }

		[DisplayName("Email")]
		[DataType(DataType.EmailAddress), MaxLength(80)]
		public string Email { get; set; }

		[DataType(DataType.Password), MaxLength(86)]
		public string PasswordHash { get; set; }

		[DataType(DataType.Password), MaxLength(16)]
		public string PasswordSalt { get; set; }

		[DisplayName("User Image"), MaxLength(100)]
		public string UserImage { get; set; }

		[DisplayName("Active"), DefaultValue(0)]
		public DeleteOffActive Active { get; set; }

		[DisplayName("Code"), MinLength(1), MaxLength(5)]
		public string Code { get; set; }

		[DisplayName("Company default")]
		[MinLength(0), MaxLength(4), DefaultValue("op")]
		public string DataAreaDefault { get; set; }

		[DisplayName("Language")]
		[MinLength(2), MaxLength(7), DefaultValue("ru")]
		public string LanguageId { get; set; }
	}
}
