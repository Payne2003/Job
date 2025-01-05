using Azure;
using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using X.PagedList;
using X.PagedList.Extensions;

namespace C___MVC.Controllers
{
    public class RecruiterController : Controller
    {
        private readonly JobContext _context;
        public RecruiterController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstRecruiter = _context.Recruiters.Include(l => l.Links)
                .Include(l => l.Skills).AsNoTracking().OrderBy(x=>x.RecruiterId);

            PagedList<Recruiter> lst = new PagedList<Recruiter>(lstRecruiter, pageNumber, pageSize);

            return View(lst);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recruiter recruiter)
        {
            // Kiểm tra nếu Username hoặc Email để trống
            if (string.IsNullOrWhiteSpace(recruiter.UserName) || string.IsNullOrWhiteSpace(recruiter.Email))
            {
                TempData["error"] = "Username and Email cannot be empty.";
                return View(recruiter); // Trả về View với thông báo lỗi
            }
            // Kiểm tra nếu email đã tồn tại trong cơ sở dữ liệu
            var existingRecruiter = await _context.Recruiters.FirstOrDefaultAsync(r => r.Email == recruiter.Email);

            if (existingRecruiter != null)
            {
                // Nếu email đã tồn tại, trả về thông báo lỗi
                TempData["error"] = "This email is already registered. Please use a different email.";
                return RedirectToAction("Create");
            }

            if (!ModelState.IsValid)
            {
                // Mã hóa mật khẩu
                recruiter.Password = HashPassword(recruiter.Password);

                // Gán giá trị mặc định cho Avatar nếu không có ảnh
                recruiter.Avatar = "avatar_user.png";
                recruiter.EmailConfirmed = true;

                // Thêm Recruiter vào cơ sở dữ liệu
                _context.Recruiters.Add(recruiter);
                await _context.SaveChangesAsync();

                // Thêm thông báo thành công vào TempData
                TempData["success"] = "Recruiter has been successfully created.";
                return RedirectToAction("Index");
            }

            // Nếu có lỗi, trả về thông báo lỗi
            TempData["error"] = "Failed to create recruiter. Please check the input data.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Tìm Recruiter và bao gồm các liên kết với Jobs, Links và Skills
            var recruiter = _context.Recruiters
                                          .Include(r => r.PostedJobs)   // Các công việc (Jobs)
                                          .Include(r => r.Links)        // Các liên kết (Links)
                                          .Include(r => r.Skills)       // Các kỹ năng (Skills)
                                          .First(r => r.RecruiterId == id);

            if (recruiter == null)
            {
                TempData["error"] = "Recruiter not found.";
                return Json(new { success = false });
            }

            // Xóa các công việc (Jobs) đã đăng bởi Recruiter
            if (recruiter.PostedJobs != null && recruiter.PostedJobs.Any())
            {
                _context.Jobs.RemoveRange(recruiter.PostedJobs);
            }

            // Xóa các liên kết (Links) liên quan đến Recruiter
            if (recruiter.Links != null && recruiter.Links.Any())
            {
                _context.Links.RemoveRange(recruiter.Links);
            }

            // Xóa các kỹ năng (Skills) liên quan đến Recruiter
            if (recruiter.Skills != null && recruiter.Skills.Any())
            {
                _context.Skills.RemoveRange(recruiter.Skills);
            }

            // Xóa Recruiter
            _context.Recruiters.Remove(recruiter);
            _context.SaveChanges();

            TempData["success"] = "Recruiter and related Jobs, Links, and Skills have been successfully deleted.";
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> GetRecruiterDetails([FromBody] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid recruiter ID.");
            }

            // Lấy Recruiter bao gồm cả Links và Skills
            var recruiter = await _context.Recruiters
                .Include(r => r.Links)     // Bao gồm dữ liệu Links
                .Include(r => r.Skills)    // Bao gồm dữ liệu Skills
                .Where(r => r.RecruiterId == id)
                .Select(r => new
                {
                    r.RecruiterId,
                    r.UserName,
                    r.Email,
                    r.PhoneNumber,
                    r.Company,
                    r.Avatar,
                    r.EmailConfirmed,
                    r.Gender,
                    r.countryside,
                    r.Currentjob,
                    r.CreatedAt,
                    r.UpdatedAt,
                    Links = r.Links.Select(l => new
                    {
                        l.Name,
                        l.Description
                    }),
                    Skills = r.Skills.Select(s => new
                    {
                        s.Name,
                        s.Description
                    })
                })
                .FirstOrDefaultAsync();

            if (recruiter == null)
            {
                return NotFound("Recruiter not found.");
            }

            return Json(recruiter); // Trả về JSON chứa tất cả thông tin
        }


        [HttpGet]
        public IActionResult Search(string? query, int? page)
        {
            int pageSize = 3; // Số lượng recruiters mỗi trang
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Xử lý query null hoặc chuỗi trắng
            query = query?.Trim() ?? "";

            // Truy vấn với điều kiện lọc và phân trang
            var recruiters = _context.Recruiters
                .Include(l => l.Links)
                .Include(l => l.Skills)
                .Where(r => string.IsNullOrEmpty(query)
                            || r.PhoneNumber.Contains(query)
                            || r.UserName.Contains(query)
                            || r.Email.Contains(query))
                .AsNoTracking()
                .OrderBy(r => r.UserName) // Sắp xếp theo tên người dùng
                .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(recruiters); // Trả về view với danh sách đã phân trang
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        [HttpPost]
        public IActionResult SendEmailToCandidate(int candidateId, string subject, string message)
        {
            // Lấy thông tin của ứng viên từ cơ sở dữ liệu
            var candidate = _context.Candidates.Find(candidateId);
            if (candidate == null)
            {
                return NotFound();
            }

            // Lấy thông tin của nhà tuyển dụng từ cơ sở dữ liệu
            var recruiterEmail = HttpContext.Session.GetString("email"); // Hoặc lấy từ UserManager nếu dùng Identity framework
            if (string.IsNullOrEmpty(recruiterEmail))
            {
              
                    return Json(new { success = false, redirectUrl = Url.Action("Login", "Auth") });
              

            }

            // Thực hiện gửi email
            try
            {
                SendEmail(recruiterEmail, candidate.Email, subject, message);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return Json(new { success = false, message = ex.Message });
            }
        }


        // Hàm gửi email
        private void SendEmail(string fromEmail, string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.your-email-provider.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@example.com", "password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
        }

    }
}
