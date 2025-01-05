using C___MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Dto
{
	public class CandidateDto
	{
		public int CandidateId { get; set; }

		public string? UserName { get; set; }

		public string? Email { get; set; }

		[Phone]
		public string? PhoneNumber { get; set; }

		public Gender? Gender { get; set; }// Giả sử bạn sử dụng string cho giới tính

		public string? Countryside { get; set; }

		public string? Currentjob { get; set; }
		public IFormFile? Avatar { get; set; }
		public string? Company { get; set; }
		public IFormFile? Cv { get; set; }
		public List<LinkDto> Links { get; set; } = new List<LinkDto>();

		// Danh sách Skills
		public List<SkillDto> Skills { get; set; } = new List<SkillDto>();

		// Danh sách Resumes
		public List<ResumeDto> Resumes { get; set; } = new List<ResumeDto>();

	}
	public class LinkDto
	{
		public int LinkId { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
	}

	// Dto cho Skill
	public class SkillDto
	{
		public int SkillId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}


}
