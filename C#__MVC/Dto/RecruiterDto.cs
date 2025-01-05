using C___MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Dto
{
	public class RecruiterDto
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
		
	}
}
