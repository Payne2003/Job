using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace C___MVC.Models
{
	[Table("SavedJob")]
	[PrimaryKey("JobId", "CandidateId")]
	public class SavedJob
	{

		[Key]
		[Column(Order = 1)]
		public int JobId { get; set; }

		[Key]
		[Column(Order = 2)]
		public int CandidateId { get; set; }

		public Candidate Candidate { get; set; }
		public Job Job { get; set; }

		[Required]
		public DateTime SavedAt { get; set; } = DateTime.Now;
	}
}
