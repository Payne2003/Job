﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C___MVC.Models
{
    [Table("CV")]
    public class CV
    {
        // Thông tin cá nhân
        [Key]
        public int ID { get; set; }
        public int CandidateId { get; set; }  // Có thể null nếu cần
        public Candidate Candidate { get; set; }  // Thuộc tính điều hướng

        // Khóa ngoại tới bảng CVTemplate
        public int TemplateId { get; set; }  // Có thể null nếu cần
        public CvTemplate Template { get; set; }
        [Required]
        public string Name { get; set; } 
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? DateOfBirth { get; set; }

        // Danh thiếp
        public string? BusinessCard { get; set; }

        // Mục tiêu nghề nghiệp
        public string? CareerObjective { get; set; }

        // Ảnh đại diện
        public string? ProfilePicture { get; set; }

        // Kinh nghiệm làm việc
        public string? WorkExperience_CompanyName { get; set; }
        public string? WorkExperience_Position { get; set; }
        public string? WorkExperience_StartDate { get; set; }
        public string? WorkExperience_EndDate { get; set; }
        public string? WorkExperience_Description { get; set; }

        // Học vấn
        public string? Education_SchoolName { get; set; }
        public string? Education_Degree { get; set; }
        public string? Education_StartDate { get; set; }
        public string? Education_EndDate { get; set; }
        public string? Education_Major { get; set; }

        // Kỹ năng
        public string? SkillsName { get; set; }
        public string? Skills { get; set; }

        // Chứng chỉ
        public string? Certificate_CertificateName { get; set; }
        public string? Certificate_IssuedDate { get; set; }

        // Dự án
        public string? Project_ProjectName { get; set; }
        public string? Project_Description { get; set; }
        public string? Project_Role { get; set; }
        public string? Project_StartDate { get; set; }
        public string? Project_EndDate { get; set; }

        // Hoạt động
        public string? Activity_ActivityName { get; set; }
        public string? Activity_Description { get; set; }

        // Sở thích
        public string? Hobbies { get; set; }

        // Người tham chiếu
        public string? Reference_Name { get; set; }
        public string? Reference_Position { get; set; }
        public string? Reference_Company { get; set; }
        public string? Reference_ContactInfo { get; set; }

        // Giải thưởng
        public string? Award_AwardName { get; set; }
        public string? Award_Date { get; set; }
       
    }
}