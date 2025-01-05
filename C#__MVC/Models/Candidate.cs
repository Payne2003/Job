using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
    [Table("Candidate")]
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]

        [StringLength(255, MinimumLength = 6)]
        public string Password { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Cv { get; set; }
        public string? countryside { get; set; }
        public string? Currentjob { get; set; }

        public string? Company { get; set; }
		public bool EmailConfirmed { get; set; } = false; // Trạng thái xác nhận email
        [StringLength(6)]
        public string? OtpCode { get; set; }

        // Thời gian hết hạn của OTP
        public DateTime? OtpExpiryTime { get; set; } = DateTime.Now;


        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? Avatar { get; set; }
		public Gender? Gender { get; set; }

		public ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();
		public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
        // Quan hệ một-nhiều với Link
        public ICollection<Link> Links { get; set; } = new List<Link>();
        // Thêm danh sách các kỹ năng
        public ICollection<Skill> Skills { get; set; }   // Một Job có nhiều Skill
        public ICollection<CV> CVs { get; set; } = new List<CV>();

    }

}
