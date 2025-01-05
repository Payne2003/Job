using System.ComponentModel.DataAnnotations;

namespace C___MVC.Dto
{
	public class JobRequestDto
	{
		[Required(ErrorMessage = "Job Title is required.")]
		[StringLength(255, ErrorMessage = "Job Title cannot exceed 255 characters.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Job Description is required.")]
		public string Description { get; set; }

		public string Experience { get; set; }

		[Required(ErrorMessage = "Company name is required.")]
		[StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
		public string Company { get; set; }

		[Required(ErrorMessage = "Quantity is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
		public int Quantity { get; set; }

		[Required(ErrorMessage = "Location is required.")]
		public int LocationId { get; set; } // Thay đổi từ string sang int

		[Required(ErrorMessage = "Salary is required.")]
		[RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Salary must be a number with up to 2 decimal places.")]
		public decimal Salary { get; set; }

		[Required(ErrorMessage = "Recruiter ID is required.")]
		public int RecruiterId { get; set; }

		[Required(ErrorMessage = "Category is required.")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Expiration Date is required.")]
		[DataType(DataType.DateTime)]
		public DateTime ExpiredAt { get; set; }

		public string? Image { get; set; }
	}
}
