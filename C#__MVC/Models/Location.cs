using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
	[Table("Location")]
	public class Location
	{
		[Key]
		public int LocationId { get; set; }

		[Required]
		[StringLength(100)]
		public string Province { get; set; }  // Ví dụ: Tỉnh/Thành phố

		// Quan hệ một-nhiều với Job
		public ICollection<Job> Jobs { get; set; }
	}
}
