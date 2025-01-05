using System.ComponentModel.DataAnnotations;

namespace C___MVC.Dto
{
	public class ForgotPasswordModel
	{
		[Required(ErrorMessage = "Email là bắt buộc.")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ.")]
		public string Email { get; set; }
	}
}
