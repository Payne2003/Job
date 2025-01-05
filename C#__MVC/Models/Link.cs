using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
	[Table("Link")]
	public class Link
	{
		[Key]
		public int LinkId { get; set; }

		[Required]
		[StringLength(255)]
		public string Description { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; } // Thêm thuộc tính Name

		// Khoá ngoại đến Job
		public int? JobId { get; set; }
		public Job Job { get; set; }

		// Thêm khoá ngoại đến Candidate
		public int? CandidateId { get; set; } // Sử dụng nullable nếu không phải lúc nào cũng có Candidate
		public Candidate Candidate { get; set; }

		// Thêm khoá ngoại đến Recruiter
		public int? RecruiterId { get; set; } // Sử dụng nullable nếu không phải lúc nào cũng có Recruiter
		public Recruiter Recruiter { get; set; }
	}

}
