using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace C___MVC.Controllers
{
	public class ProfileController : Controller
	{
		private readonly JobContext _context;
		public ProfileController(JobContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult UploadAvatar(IFormFile avatar)
		{
			if (avatar != null && avatar.Length > 0)
			{
				// Đường dẫn lưu trữ file ảnh
				var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

				// Tạo tên file mới bằng Guid và giữ nguyên phần mở rộng của file gốc
				var fileExtension = Path.GetExtension(avatar.FileName);
				var fileName = Guid.NewGuid().ToString() + fileExtension; // Đổi tên file

				// Đường dẫn lưu file
				var filePath = Path.Combine(uploads, fileName);

				// Lưu file vào thư mục "wwwroot/images"
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					avatar.CopyTo(fileStream);
				}

				// Trả về tên file mới cho client để hiển thị
				return Json(new { fileName });
			}

			return BadRequest();
		}

		public IActionResult Candidate()
		{
			// Lấy CandidateId từ session
			int candidateId = GetCandidateIdFromSession();

			// Kiểm tra nếu không có CandidateId (tức là không đăng nhập hoặc không tồn tại)
			if (candidateId == 0)
			{
				return RedirectToAction("Login", "Auth");
			}


			// Tìm kiếm ứng viên dựa trên CandidateId
			var candidate = _context.Candidates
				.Include(c => c.Resumes)
				.Include(c => c.Links)
				.Include(c => c.Skills)// Nếu cần bao gồm danh sách CV của ứng viên
				.FirstOrDefault(c => c.CandidateId == candidateId);

			// Nếu không tìm thấy ứng viên
			if (candidate == null)
			{
				return NotFound("Candidate not found.");
			}

			// Trả về view với model là thông tin ứng viên
			return View(candidate);
		}
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Candidate(CandidateDto candidateDto, IFormFile? avatar)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		int candidateId = GetCandidateIdFromSession();
		//		var existingCandidate = _context.Candidates.Find(candidateId);

		//		if (existingCandidate == null)
		//		{
		//			return NotFound();
		//		}

		//		// Kiểm tra nếu có avatar mới được upload
		//		if (avatar != null && avatar.Length > 0)
		//		{
		//			var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
		//			var fileExtension = Path.GetExtension(avatar.FileName);
		//			var fileName = Guid.NewGuid().ToString() + fileExtension;
		//			var filePath = Path.Combine(uploads, fileName);

		//			using (var fileStream = new FileStream(filePath, FileMode.Create))
		//			{
		//				await avatar.CopyToAsync(fileStream);
		//			}

		//			// Xóa avatar cũ nếu cần
		//			if (!string.IsNullOrEmpty(existingCandidate.Avatar) && existingCandidate.Avatar != "avatar_user.png")
		//			{
		//				var oldFilePath = Path.Combine(uploads, existingCandidate.Avatar);
		//				if (System.IO.File.Exists(oldFilePath))
		//				{
		//					System.IO.File.Delete(oldFilePath);
		//				}
		//			}

		//			existingCandidate.Avatar = fileName;
		//		}

		//		existingCandidate.Gender = candidateDto.Gender ?? existingCandidate.Gender;
		//		// Cập nhật các thuộc tính khác
		//		existingCandidate.UserName = candidateDto.UserName ?? existingCandidate.UserName;
		//		existingCandidate.Email = candidateDto.Email ?? existingCandidate.Email;
		//		existingCandidate.PhoneNumber = candidateDto.PhoneNumber ?? existingCandidate.PhoneNumber;
		//		existingCandidate.Company = candidateDto.Company ?? existingCandidate.Company;
		//		existingCandidate.countryside = candidateDto.Countryside ?? existingCandidate.countryside;
		//		existingCandidate.Currentjob = candidateDto.Currentjob ?? existingCandidate.Currentjob;

		//		existingCandidate.UpdatedAt = DateTime.Now;

		//		_context.Candidates.Update(existingCandidate);
		//		await _context.SaveChangesAsync();

		//		return RedirectToAction("Candidate");
		//	}

		//	// Nếu ModelState không hợp lệ, trả về view với thông báo lỗi
		//	return View(candidateDto);
		//}


		// Action để hiển thị trang hồ sơ của nhà tuyển dụng (Recruiter)
		public IActionResult Recruiter()
		{
			// Lấy RecruiterId từ session
			int recruiterId = GetRecruiterIdFromSession();

			// Kiểm tra nếu không có RecruiterId (tức là không đăng nhập hoặc không tồn tại)
			if (recruiterId == 0)
			{
				return RedirectToAction("Login", "Auth");
			}

			// Tìm kiếm nhà tuyển dụng dựa trên RecruiterId
			var recruiter = _context.Recruiters
				.Include(r => r.PostedJobs)
				.Include(c => c.Links)
				.Include(c => c.Skills)// Nếu cần bao gồm danh sách công việc đã đăng
				.FirstOrDefault(r => r.RecruiterId == recruiterId);

			// Nếu không tìm thấy nhà tuyển dụng
			if (recruiter == null)
			{
				return NotFound("Recruiter not found.");
			}

			// Trả về view với model là thông tin nhà tuyển dụng
			return View(recruiter);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Candidate(CandidateDto candidateDto)
		{
			if (ModelState.IsValid)
			{
				int candidateId = GetCandidateIdFromSession();
				var existingCandidate = await _context.Candidates.FindAsync(candidateId);
				if (existingCandidate == null) return NotFound();

				// Upload avatar nếu có file mới, nếu không giữ nguyên
				if (candidateDto.Avatar != null && candidateDto.Avatar.Length > 0)
				{
					var avatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", candidateDto.Avatar.FileName);
					using (var stream = new FileStream(avatarPath, FileMode.Create))
					{
						await candidateDto.Avatar.CopyToAsync(stream);
					}
					existingCandidate.Avatar = candidateDto.Avatar.FileName;
				}

				// Upload CV nếu có file mới, nếu không giữ nguyên
				if (candidateDto.Cv != null && candidateDto.Cv.Length > 0)
				{
					var cvPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", candidateDto.Cv.FileName);
					using (var stream = new FileStream(cvPath, FileMode.Create))
					{
						await candidateDto.Cv.CopyToAsync(stream);
					}
					existingCandidate.Cv = candidateDto.Cv.FileName;
				}

				// Cập nhật thông tin khác
				existingCandidate.UserName = candidateDto.UserName ?? existingCandidate.UserName;
				existingCandidate.Email = candidateDto.Email ?? existingCandidate.Email;
				existingCandidate.PhoneNumber = candidateDto.PhoneNumber ?? existingCandidate.PhoneNumber;
				existingCandidate.Gender = candidateDto.Gender ?? existingCandidate.Gender;
				existingCandidate.countryside = candidateDto.Countryside ?? existingCandidate.countryside;
				existingCandidate.Currentjob = candidateDto.Currentjob ?? existingCandidate.Currentjob;
				existingCandidate.Company = candidateDto.Company ?? existingCandidate.Company;

				_context.Candidates.Update(existingCandidate);
				await _context.SaveChangesAsync();

				return RedirectToAction("Candidate");
			}

			return View(candidateDto);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Recruiter(RecruiterDto recruiterDto)
		{
			if (ModelState.IsValid)
			{
				int recruiterId = GetRecruiterIdFromSession();
				var existingRecruiter = await _context.Recruiters.FindAsync(recruiterId);
				if (existingRecruiter == null) return NotFound();

				// Upload avatar nếu có file mới, nếu không giữ nguyên
				if (recruiterDto.Avatar != null && recruiterDto.Avatar.Length > 0)
				{
					var avatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", recruiterDto.Avatar.FileName);
					using (var stream = new FileStream(avatarPath, FileMode.Create))
					{
						await recruiterDto.Avatar.CopyToAsync(stream);
					}
					existingRecruiter.Avatar = recruiterDto.Avatar.FileName;
				}



				// Cập nhật thông tin khác
				existingRecruiter.UserName = recruiterDto.UserName ?? existingRecruiter.UserName;
				existingRecruiter.Email = recruiterDto.Email ?? existingRecruiter.Email;
				existingRecruiter.PhoneNumber = recruiterDto.PhoneNumber ?? existingRecruiter.PhoneNumber;
				existingRecruiter.Gender = recruiterDto.Gender ?? existingRecruiter.Gender;
				existingRecruiter.countryside = recruiterDto.Countryside ?? existingRecruiter.countryside;
				existingRecruiter.Currentjob = recruiterDto.Currentjob ?? existingRecruiter.Currentjob;
				existingRecruiter.Company = recruiterDto.Company ?? existingRecruiter.Company;

				_context.Recruiters.Update(existingRecruiter);
				await _context.SaveChangesAsync();

				return RedirectToAction("Recruiter");
			}

			return View(recruiterDto);
		}

		private int GetCandidateIdFromSession()
		{
			var candidateEmail = HttpContext.Session.GetString("email");
			var candidate = _context.Candidates.FirstOrDefault(c => c.Email == candidateEmail);
			return candidate?.CandidateId ?? 0;
		}
		private int GetRecruiterIdFromSession()
		{
			var recruiterEmail = HttpContext.Session.GetString("email");
			var recruiter = _context.Recruiters.FirstOrDefault(c => c.Email == recruiterEmail);
			return recruiter?.RecruiterId ?? 0;
		}

	}
}
