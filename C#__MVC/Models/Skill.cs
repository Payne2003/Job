using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C___MVC.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }  // Khóa chính của Skill

        [Required]
        [StringLength(100)]  // Đặt giới hạn độ dài cho tên kỹ năng
        public string Name { get; set; }  // Tên kỹ năng
        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        // Khóa ngoại liên kết với bảng Job

        public int? JobId { get; set; }    // Khóa ngoại đến Job
        public Job Job { get; set; }
		public int? CandidateId { get; set; } // Sử dụng nullable nếu không phải lúc nào cũng có Candidate
		public Candidate Candidate { get; set; }

		// Thêm khoá ngoại đến Recruiter
		public int? RecruiterId { get; set; } // Sử dụng nullable nếu không phải lúc nào cũng có Recruiter
		public Recruiter Recruiter { get; set; }// Điều hướng ngược về Job
	}

}
