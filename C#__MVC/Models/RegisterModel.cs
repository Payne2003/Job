using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
	public class RegisterModel
	{
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email không được trống")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password không được trống")]
		[DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        public string Password { get; set; }
        public string? Company { get; set; }
        [Phone]
		public string? PhoneNumber { get; set; }
		[Required(ErrorMessage = "Xác nhận mật khẩu không được trống")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Giới tính không được trống")]
		public Gender? Gender { get; set; }
	}
}
