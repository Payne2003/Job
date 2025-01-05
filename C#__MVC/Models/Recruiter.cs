using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C___MVC.Models
{
    [Table("Recruiter")]
    public class Recruiter
    {
        [Key]
        public int RecruiterId { get; set; }
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


        public string? Company { get; set; }
		public bool EmailConfirmed { get; set; } = false; // Trạng thái xác nhận email
        [StringLength(6)]
        public string? OtpCode { get; set; }

        // Thời gian hết hạn của OTP
        public DateTime? OtpExpiryTime { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? Avatar { get; set; }
		public Gender? Gender { get; set; }
        public string? countryside { get; set; }
        public string? Currentjob { get; set; }
        public ICollection<Job> PostedJobs { get; set; } = new List<Job>();
        // Quan hệ một-nhiều với Link
        public ICollection<Link> Links { get; set; } = new List<Link>();
        // Thêm danh sách các kỹ năng
        public ICollection<Skill> Skills { get; set; }   // Một Job có nhiều Skill

    }
    public enum Gender
	{
		Male,
		Female,
		Other,
        HasValue
    }
}
