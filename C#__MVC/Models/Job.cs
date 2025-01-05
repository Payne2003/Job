using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
    [Table("Job")]
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Experience { get; set; }

        [Required]
        [StringLength(100)]
        public string Company { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Mức lương phải là số và có tối đa 2 chữ số thập phân")]
        public decimal Salary { get; set; }

        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpiredAt { get; set; }

        public string? Image { get; set; }

        [Required]
        public ICollection<Resume> Resumes { get; set; } = new List<Resume>();

        // Quan hệ một-nhiều với Link
        public ICollection<Link> Links { get; set; } = new List<Link>();
        // Thêm danh sách các kỹ năng
        public ICollection<Skill> Skills { get; set; }   // Một Job có nhiều Skill
		public ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();

		// Khóa ngoại đến Location
		[Required]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        // Thêm thuộc tính Status
        [Required]
        public bool Status { get; set; } = false; // Giá trị mặc định
    }
}
