using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace C___MVC.Controllers
{
    public class ResumeController : Controller
    {
        private readonly JobContext _context;
        public ResumeController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyAsync(ResumeDto model)
        {
			int candidate_Id = GetCandidateIdFromSession();
            int recruiter_id = GetRecruiterIdFromSession();
            // Kiểm tra nếu candidate_Id hợp lệ
            if (candidate_Id == 0 )
            {
				TempData["error"] = "Hãy đăng nhập với tư cách candidate";
				return RedirectToAction("Login", "Auth");		
			}
            else
            {
				var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == model.JobId);

				if (job == null)
				{
					TempData["error"] = "Công việc không tồn tại!";
					return RedirectToAction("Index", "Home");
				}

				// Kiểm tra nếu ngày hết hạn đã qua
				if (job.ExpiredAt < DateTime.Now)
				{
					TempData["error"] = "Công việc này đã hết hạn, bạn không thể nộp đơn!";
					return RedirectToAction("Details", "Job", new { id = model.JobId });
				}
				var existingResume = await _context.Resumes
			    .FirstOrDefaultAsync(r => r.JobId == model.JobId && r.CandidateId == candidate_Id);

				if (existingResume != null)
				{
					TempData["error"] = "Bạn đã nộp đơn vào công việc này trước đó!";
					return RedirectToAction("Details", "Job", new { id = model.JobId });
				}
				string cvFilePath = null;
                if (model.Cv != null && model.Cv.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.Cv.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Cv.CopyTo(stream);
                    }
                    cvFilePath = model.Cv.FileName;
                }
                // Tạo đối tượng Resume và lưu vào database
                var resume = new Resume
                {
                    JobId = model.JobId,
                    CandidateId = candidate_Id,           
                    Introduce = model.Introduce,
                    Cv = cvFilePath,
                    CreatedAt = DateTime.Now
                };

                _context.Resumes.Add(resume);
                await _context.SaveChangesAsync();

                // Chuyển hướng về trang Details của công việc sau khi thêm thành công
                return RedirectToAction("Details", "Job", new { id = model.JobId });
            }
        }

		public IActionResult DownloadCv(string fileName)
		{
            if (string.IsNullOrEmpty(fileName))
            {
                TempData["error"] = "The file does not exist or has been deleted.";
                return RedirectToAction("Candidate", "Profile");
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            // Kiểm tra nếu file tồn tại
            if (!System.IO.File.Exists(filePath))
            {
                // Thông báo lỗi tùy thuộc vào yêu cầu (JSON hoặc chuyển hướng về View lỗi)
                TempData["error"] = "The file does not exist or has been deleted.";
                return RedirectToAction("Candidate", "Profile");
            }

            // Lấy định dạng mime type
            var mimeType = "application/octet-stream"; // Định dạng mặc định
			var extension = Path.GetExtension(filePath).ToLowerInvariant();
			if (extension == ".pdf") mimeType = "application/pdf";
			else if (extension == ".doc" || extension == ".docx") mimeType = "application/msword";
			else if (extension == ".jpg" || extension == ".jpeg") mimeType = "image/jpeg";
			else if (extension == ".png") mimeType = "image/png";

			// Trả về file để tải xuống
			return PhysicalFile(filePath, mimeType, fileName);
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
		public async Task<IActionResult> List()
        {
			int candidateId = GetCandidateIdFromSession();

			if (candidateId <= 0)
            {
                return NotFound();
            }

            var resumes = await _context.Resumes
                .Where(r => r.CandidateId == candidateId)
                .Include(r => r.Job) // Nếu bạn muốn hiển thị thông tin về công việc
                .ToListAsync();
            return View(resumes);
        }
	
		public async Task<IActionResult> ListResumebyJob(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            // Lấy thông tin tên của công việc
            var jobTitle = await _context.Jobs
                .Where(j => j.JobId == id)
                .Select(j => j.Title)
                .FirstOrDefaultAsync();
            var jobNumber = await _context.Jobs
                .Where(j => j.JobId == id)
                .Select(j => j.Quantity)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(jobTitle))
            {
                return NotFound();
            }

            // Lấy danh sách tên các ứng viên từ Resumes liên quan đến công việc
            var resume = await _context.Resumes
                .Include(r => r.Candidate)
                .Include(r => r.Job)
                .Where(r => r.JobId == id)  
                .ToListAsync();

            // Truyền tên công việc và danh sách tên ứng viên vào View
            ViewBag.JobTitle = jobTitle;
            ViewBag.JobNumber = jobNumber;
            ViewBag.JobId = id;
            return View(resume);
        }
		public IActionResult ListByRecruiter()
		{
			int recruiterId = GetRecruiterIdFromSession();

			if (recruiterId <= 0)
			{
				return NotFound();
			}

			var job = _context.Jobs
				.Where(j => j.RecruiterId == recruiterId)
				.Include(j => j.Category) // Bao gồm Category nếu cần hiển thị
				.Include(j => j.Recruiter)
				.Include(j => j.Location)
				.Include(j => j.Links)    // Bao gồm thông tin về Link
				.Include(j => j.Skills) // Nếu bạn muốn hiển thị thông tin về công việc
				.ToList();

	
			var JobRecruiterName = _context.Jobs
				.Where(j => j.RecruiterId == recruiterId)
				.Select(j => j.Recruiter.UserName)
				.FirstOrDefault();

			ViewBag.RecruiterName = JobRecruiterName;

			return View(job);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteResume(int jobId, int candidateId, string returnUrl)
		{
			var resume = await _context.Resumes
				.FirstOrDefaultAsync(r => r.JobId == jobId && r.CandidateId == candidateId);

			if (resume == null)
			{
				return RedirectToAction(returnUrl); // Điều hướng đến trang được chỉ định
			}

			_context.Resumes.Remove(resume);
			await _context.SaveChangesAsync();

		

			// Điều hướng lại trang dựa vào returnUrl
			if (!string.IsNullOrEmpty(returnUrl))
			{
				return RedirectToAction(returnUrl);
			}
			else
			{
				// Nếu không có returnUrl, bạn có thể điều hướng về một trang mặc định
				return RedirectToAction("List");
			}
		}

		[HttpPost]
		public IActionResult AcceptResume(int jobId, int candidateId)
		{
			var resume = _context.Resumes.FirstOrDefault(r => r.JobId == jobId && r.CandidateId == candidateId);

			if (resume != null)
			{
				resume.Status = ApplicationStatus.Accepted;
				_context.SaveChanges();
			}

			// Redirect to ListResumeByJob with jobId as a route parameter
			return RedirectToAction("ListResumeByJob", new { id = jobId }); // Sử dụng "id" thay vì "jobId"
		}
		[HttpPost]
		public IActionResult RejectResume(int jobId, int candidateId)
		{
			var resume = _context.Resumes.FirstOrDefault(r => r.JobId == jobId && r.CandidateId == candidateId);

			if (resume != null)
			{
				resume.Status = ApplicationStatus.Rejected;
				_context.SaveChanges();
			}

			// Redirect to ListResumeByJob with jobId as a route parameter
			return RedirectToAction("ListResumeByJob", new { id = jobId }); // Sử dụng "id" thay vì "jobId"
		}

		[HttpPost]
		public IActionResult SearchCandidates(int jobId, string keyword)
		{
			// Nếu từ khóa rỗng, trả về danh sách ứng viên của Job hiện tại
			var resumes = _context.Resumes
				.Include(r => r.Candidate)
				.Include(r => r.Job)
				.Where(r => r.Job.JobId == jobId); // Lọc theo JobId

			if (!string.IsNullOrEmpty(keyword))
			{
				// Lọc thêm theo UserName hoặc Email nếu có từ khóa
				resumes = resumes.Where(r => r.Candidate.UserName.Contains(keyword) || r.Candidate.Email.Contains(keyword));
			}

			// Lấy danh sách kết quả
			var result = resumes.ToList();

			// Trả về PartialView cùng danh sách kết quả
			return PartialView("_CandidateList", result);
		}

		[HttpPost]
		public IActionResult SearchCandidatesbyjob(string keyword)
		{
			// Nếu từ khóa rỗng, trả về danh sách rỗng
			if (string.IsNullOrEmpty(keyword))
			{
				return PartialView("_CandidateList", new List<Resume>());
			}

			// Tìm kiếm trong bảng Resume và liên kết bảng Job theo Job Title
			var resumes = _context.Resumes
				.Include(r => r.Candidate) // Bao gồm thông tin ứng viên
				.Include(r => r.Job)       // Bao gồm thông tin công việc
				.Where(r => r.Job.Title.Contains(keyword)) // Lọc theo tiêu đề công việc
				.ToList();

			// Trả về PartialView cùng danh sách kết quả
			return PartialView("_CandidateList", resumes);
		}


	}
}
