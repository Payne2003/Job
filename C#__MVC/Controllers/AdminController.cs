using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq; // Đảm bảo import namespace này

namespace C___MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly JobContext _context;

        public AdminController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Gọi hàm GetStatistics để lấy thông tin thống kê
            var statistics = GetStatistics();

            // Lấy danh sách các đối tượng khác (nếu cần)
            var recruiters = _context.Recruiters.ToList();
            var jobs = _context.Jobs.ToList();
            var categories = _context.Categories.ToList();
            var resumes = _context.Resumes.ToList();
            var candidates = _context.Candidates.Include(c => c.Links).Include(c => c.Skills).Include(c => c.Resumes).ToList();

            // Tính tỷ lệ hoàn thành cho từng ứng viên
            var candidateCompletion = new List<int>();
            foreach (var candidate in candidates)
            {
                int filledFields = 0;

                // Tính số trường đã được điền
                if (!string.IsNullOrEmpty(candidate.Avatar)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.UserName)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.Email)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.PhoneNumber)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.Cv)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.countryside)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.Currentjob)) filledFields++;
                if (!string.IsNullOrEmpty(candidate.Company)) filledFields++;
                if (candidate.Gender.HasValue) filledFields++; // Nếu có giới tính

                // Tính số kỹ năng và liên kết đã có
                if (candidate.Skills != null && candidate.Skills.Any()) filledFields += candidate.Skills.Count;
                if (candidate.Links != null && candidate.Links.Any()) filledFields += candidate.Links.Count;

                // Tính tỷ lệ hoàn thành
                int totalFields = 11;
                int completionPercentage = (filledFields * 100) / totalFields;

                candidateCompletion.Add(completionPercentage);
            }

            // Đưa tỷ lệ hoàn thành vào ViewBag
            ViewBag.CandidateCompletion = candidateCompletion;

            // Tính tỷ lệ hoàn thành cho từng recruiter
            var recruiterCompletion = new List<int>();
            foreach (var recruiter in recruiters)
            {
                int filledFields = 0;

                // Tính số trường đã được điền cho recruiter
                if (!string.IsNullOrEmpty(recruiter.UserName)) filledFields++;
                if (!string.IsNullOrEmpty(recruiter.Email)) filledFields++;
                if (!string.IsNullOrEmpty(recruiter.PhoneNumber)) filledFields++;
                if (!string.IsNullOrEmpty(recruiter.Company)) filledFields++;
                if (!string.IsNullOrEmpty(recruiter.countryside)) filledFields++;
                if (!string.IsNullOrEmpty(recruiter.Currentjob)) filledFields++;
                if (!string.IsNullOrEmpty(recruiter.Avatar)) filledFields++; // Nếu có Avatar
                if (recruiter.Gender.HasValue) filledFields++; // Nếu có giới tính
                // Tính số kỹ năng và liên kết đã có
                if (recruiter.Skills != null && recruiter.Skills.Any()) filledFields += recruiter.Skills.Count;
                if (recruiter.Links != null && recruiter.Links.Any()) filledFields += recruiter.Links.Count;

                // Tính tỷ lệ hoàn thành cho recruiter
                int totalFieldsRecruiter = 10; // Tổng số trường cần thiết cho recruiter
                int recruiterCompletionPercentage = (filledFields * 100) / totalFieldsRecruiter;

                recruiterCompletion.Add(recruiterCompletionPercentage);
            }

            // Đưa tỷ lệ hoàn thành vào ViewBag cho recruiter
            ViewBag.RecruiterCompletion = recruiterCompletion;
            // Tạo mô hình cho view
            var model = new DashboardViewModel
            {
                TotalCandidates = statistics.TotalCandidates,
                TotalRecruiters = statistics.TotalRecruiters,
                CandidatesWithResume = statistics.CandidatesWithResume,
                Recruiters = recruiters,
                Jobs = jobs,
                Categories = categories,
                Resumes = resumes,
                Candidates = candidates // Thêm danh sách candidates vào model
            };

            return View(model); // Truyền mô hình vào view
        }

        private dynamic GetStatistics()
        {
            // Tính tổng số Candidate
            int totalCandidates = _context.Candidates.Count();

            // Tính tổng số Recruiter
            int totalRecruiters = _context.Recruiters.Count();

            // Tính số Candidate đã có Resume
            int candidatesWithResume = _context.Resumes.Select(r => r.CandidateId).Distinct().Count();

            var statistics = new
            {
                TotalCandidates = totalCandidates,
                TotalRecruiters = totalRecruiters,
                CandidatesWithResume = candidatesWithResume
            };

            return statistics;
        }
        public IActionResult DownloadCv(int jobId, int candidateId)
        {
            // Tìm resume dựa trên JobId và CandidateId
            var resume = _context.Resumes
                .Include(r => r.Candidate) // Bao gồm thông tin ứng viên nếu cần
                .Include(r => r.Job) // Bao gồm thông tin công việc nếu cần
                .FirstOrDefault(r => r.JobId == jobId && r.CandidateId == candidateId);

            if (resume == null || string.IsNullOrEmpty(resume.Cv))
            {
                return NotFound(); // Không tìm thấy hồ sơ
            }

            // Giả định rằng đường dẫn đến CV là một đường dẫn đến tệp trên máy chủ
            var filePath = Path.Combine("path/to/cvs", resume.Cv); // Đường dẫn đến tệp CV

            // Kiểm tra xem tệp có tồn tại không
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Tệp không tồn tại
            }

            // Trả về tệp cho người dùng tải về
            var fileType = "application/pdf"; // Định dạng tệp, có thể thay đổi nếu cần
            var fileName = Path.GetFileName(filePath);
            return PhysicalFile(filePath, fileType, fileName);
        }

        [HttpPost]
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

        [HttpGet]
        public IActionResult ProfileAdmin()
        {
            int id = GetAdminIdFromSession();
            // Giả sử bạn có DbContext để truy xuất thông tin admin từ CSDL
            var admin = _context.Admins.FirstOrDefault(a => a.Id == id);

            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            return View(admin); // Trả về đối tượng Admin cho form chỉnh sửa
        }
		[HttpPost]
		public IActionResult ProfileAdmin([Bind("Id,Name,Email,Password,ConfirmPassword")] Admin admin)
		{
			if (!ModelState.IsValid)
			{
				return View(admin); // Nếu có lỗi xác thực, trả về view với model hiện tại
			}
			var existingAdmin = _context.Admins.FirstOrDefault(a => a.Id == admin.Id);

			if (existingAdmin == null)
			{
				return NotFound("Admin not found.");
			}
			existingAdmin.Name = admin.Name;
			existingAdmin.Email = admin.Email;
			if (!string.IsNullOrEmpty(admin.Password) && admin.Password == admin.ConfirmPassword)
			{
				existingAdmin.Password = admin.Password;
			}
			else if (!string.IsNullOrEmpty(admin.Password))
			{
				ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
				return View(admin);
			}

			// Lưu thay đổi vào cơ sở dữ liệu
			_context.Update(existingAdmin);
			_context.SaveChanges();

			return RedirectToAction("ProfileAdmin"); // Quay lại trang Profile sau khi cập nhật thành công
		}

		private int GetAdminIdFromSession()
        {
            var adminEmail = HttpContext.Session.GetString("email");
            var admin = _context.Admins.FirstOrDefault(c => c.Email == adminEmail);
            return admin?.Id ?? 0;
        }
    }
}
