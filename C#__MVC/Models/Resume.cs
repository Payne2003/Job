using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
    [Table("Resume")]
    [PrimaryKey("JobId", "CandidateId")]
    public class Resume
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
        public string Cv { get; set; }

        // Thuộc tính mới Introduce
        [StringLength(500, ErrorMessage = "Introduce cannot exceed 500 characters")]
        public string? Introduce { get; set; }

    
        public DateTime CreatedAt { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    }

    public enum ApplicationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
