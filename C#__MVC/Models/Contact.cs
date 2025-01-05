using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C___MVC.Models
{
	[Table("Contact")]
	public class Contact
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }

		[Phone]
		[StringLength(15)]
		public string Telephone { get; set; }

		[Required]
		[StringLength(200)]
		public string Subject { get; set; }

		[Required]
		public string Message { get; set; }
	}
}
