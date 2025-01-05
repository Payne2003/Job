using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.Win32;
using System.Net.Mail;
using System.Net;
using BCrypt.Net;
using C___MVC.Dto;
using BCrypt.Net;
using System;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace C___MVC.Controllers
{
	public class AuthController : Controller
	{
		private readonly JobContext _context;
		public AuthController(JobContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Login()
		{
			// Nếu người dùng đã đăng nhập, chuyển hướng về trang chủ
			if (HttpContext.Session.GetString("email") != null)
			{
				return RedirectToAction("Index", "Home");
			}

			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{

                // Kiểm tra trong bảng Candidate
                var candidate = _context.Candidates.FirstOrDefault(c => c.Email == model.Email);
                if (candidate != null)
                {
                    if (!candidate.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Email của bạn chưa được xác nhận.");
                        return View(model); // Trả về view cùng thông báo lỗi
                    }
                    // Xác thực mật khẩu bằng BCrypt
                    if (BCrypt.Net.BCrypt.Verify(model.Password, candidate.Password))
                    {
                        // Lưu thông tin vào session
                        HttpContext.Session.SetString("email", candidate.Email);
                        HttpContext.Session.SetString("accountType", "Candidate");
                        HttpContext.Session.SetInt32("candidateId", candidate.CandidateId);
                        return RedirectToAction("Index", "Home"); // Chuyển về trang chủ sau khi đăng nhập thành công
                    }
                }

                // Kiểm tra trong bảng Recruiter nếu không phải là Candidate
                var recruiter = _context.Recruiters.FirstOrDefault(r => r.Email == model.Email);
                if (recruiter != null)
                {
                    if (!recruiter.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Email của bạn chưa được xác nhận.");
                        return View(model); // Trả về view cùng thông báo lỗi
                    }
                    // Xác thực mật khẩu bằng BCrypt
                    if (BCrypt.Net.BCrypt.Verify(model.Password, recruiter.Password))
                    {
                        // Lưu thông tin vào session
                        HttpContext.Session.SetString("email", recruiter.Email);
                        HttpContext.Session.SetString("accountType", "Recruiter");
                        HttpContext.Session.SetInt32("recruiterId", recruiter.RecruiterId);
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Kiểm tra trong bảng Admin nếu không phải là hai trường trên
                var admin = _context.Admins
					.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
				if (admin != null)
				{
					// Lưu thông tin vào session
					HttpContext.Session.SetString("email", admin.Email);
					HttpContext.Session.SetString("accountType", "Admin");
					// Chuyển hướng đến trang Admin
					return RedirectToAction("Index", "Admin");
				}

				// Nếu không tìm thấy thông tin đăng nhập đúng
				ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác.");
			}

			// Nếu có lỗi, trả về view đăng nhập cùng thông báo lỗi
			return View(model);
		}

		[HttpGet]
		public IActionResult CheckAuthentication()
		{
			string email = HttpContext.Session.GetString("email");

			if (!string.IsNullOrEmpty(email))
			{
				// Kiểm tra bảng Candidate
				var candidate = _context.Candidates.FirstOrDefault(c => c.Email == email);
				if (candidate != null)
				{
					return new JsonResult(new
					{
						isAuthenticated = true,
						isCandidate = true,
						isRecruiter = false,
						avatar = candidate.Avatar // Giả sử bạn có trường Avatar trong Candidate
					});
				}

				// Kiểm tra bảng Recruiter
				var recruiter = _context.Recruiters.FirstOrDefault(r => r.Email == email);
				if (recruiter != null)
				{
					return new JsonResult(new
					{
						isAuthenticated = true,
						isCandidate = false,
						isRecruiter = true,
						avatar = recruiter.Avatar // Giả sử bạn có trường Avatar trong Recruiter
					});
				}
			}

			// Nếu không tìm thấy email hoặc người dùng chưa đăng nhập
			return new JsonResult(new { isAuthenticated = false });
		}
        public IActionResult CheckAuthenticationAdmin()
        {
            // Lấy email từ session
            string email = HttpContext.Session.GetString("email");

            // Kiểm tra nếu email tồn tại trong session
            if (!string.IsNullOrEmpty(email))
            {
                // Kiểm tra bảng Admin
                var admin = _context.Admins.FirstOrDefault(a => a.Email == email);
                if (admin != null)
                {
                    // Trả về JSON có thêm thông tin Tên
                    return new JsonResult(new
                    {
                        isAuthenticated = true,
                        isAdmin = true,
                        name = admin.Name // Lấy tên của Admin
                    });
                }
            }

            // Nếu không tìm thấy email hoặc người dùng chưa đăng nhập
            return new JsonResult(new { isAuthenticated = false });
        }



        public IActionResult RegisterCandidate()
		{
			// Lấy email từ session
			string email = HttpContext.Session.GetString("email");

			// Kiểm tra email trong session, nếu không có email thì người dùng chưa đăng nhập
			if (!string.IsNullOrEmpty(email))
			{				
				return RedirectToAction("Index", "Job");
								
			}

			// Nếu người dùng chưa đăng nhập, hiển thị form đăng ký
			return View();
		}
		public IActionResult RegisterRecruiter()
		{
			// Lấy email từ session
			string email = HttpContext.Session.GetString("email");

			// Kiểm tra email trong session, nếu không có email thì người dùng chưa đăng nhập
			if (!string.IsNullOrEmpty(email))
			{
								
				return RedirectToAction("Index", "Home");				
			}

			// Nếu người dùng chưa đăng nhập, hiển thị form đăng ký
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterCandidate(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email có tồn tại trong hệ thống
                var existsCandidate = _context.Candidates.Any(c => c.Email == model.Email);
                var existsRecruiter = _context.Recruiters.Any(r => r.Email == model.Email);

                if (existsCandidate || existsRecruiter)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống.");
                    return View(model);
                }

                // Kiểm tra mật khẩu có khớp không
                if (model.Password == model.ConfirmPassword)
                {
                    // Tạo mã OTP gồm 6 số ngẫu nhiên
                    var otp = new Random().Next(100000, 999999).ToString();                  
                    // Lưu thông tin Candidate với trạng thái chưa xác nhận email
                    Candidate candidate = new Candidate
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        Password = HashPassword(model.Password), // Nên sử dụng hashing
                        EmailConfirmed = false,
                        OtpCode = otp, // Lưu OTP trong cơ sở dữ liệu
                        OtpExpiryTime = DateTime.Now.AddMinutes(5), // Thời gian hết hạn của OTP (5 phút)
                        Avatar = "avatar_user.png",
                        Gender = model.Gender
                    };

                    _context.Candidates.Add(candidate);
                    _context.SaveChanges();

                    // Gửi OTP qua email
                    SendOtpEmail(model.Email, otp);

                    // Chuyển hướng tới trang nhập mã OTP
                    return RedirectToAction("ConfirmOtp", new { email = model.Email });
                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                }
            }

            return View(model);
        }

        private string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

        public IActionResult ConfirmOtp(string email)
        {
            var model = new ConfirmOtpModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOtp(ConfirmOtpModel model)
        {
            
            var candidate = _context.Candidates.SingleOrDefault(c => c.Email == model.Email && c.OtpCode == model.Otp);
            var recruiter = _context.Recruiters.SingleOrDefault(r => r.Email == model.Email && r.OtpCode == model.Otp);

            if (candidate != null && candidate.OtpExpiryTime > DateTime.Now)
            {
                // Xử lý cho Candidate
                candidate.EmailConfirmed = true;
                candidate.OtpCode = null; // Xóa OTP sau khi xác nhận thành công
                candidate.OtpExpiryTime = null;
                _context.SaveChanges();
                TempData["success"] = "Candidate added successfully!";
                return RedirectToAction("Login");
            }
            else if (recruiter != null && recruiter.OtpExpiryTime > DateTime.Now)
            {
                // Xử lý cho Recruiter
                recruiter.EmailConfirmed = true;
                recruiter.OtpCode = null; // Xóa OTP sau khi xác nhận thành công
                recruiter.OtpExpiryTime = null;
                _context.SaveChanges();
                TempData["success"] = "Recruiter has been successfully created.";
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("Otp", "Mã OTP không đúng hoặc đã hết hạn.");
            }

            // Nếu có lỗi, trả về view với thông báo lỗi và các giá trị nhập
            return View(model); // Gửi lại model về view
        }

		


		private void SendOtpEmail(string email, string otp)
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587; // Port cho TLS
                client.EnableSsl = true; // Bật SSL
                client.Credentials = new NetworkCredential(
                    Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
                    Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                );

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(Environment.GetEnvironmentVariable("EMAIL_USERNAME")),
                    Subject = "Mã OTP Xác Nhận Địa Chỉ Email",
                    Body = $"<h1>Xác Nhận Địa Chỉ Email</h1>" +
                           $"<p>Đây là mã OTP của bạn: <strong>{otp}</strong></p>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                try
                {
                    client.Send(mailMessage);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterRecruiter(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email có tồn tại trong hệ thống
                var existsCandidate = _context.Candidates.Any(c => c.Email == model.Email);
                var existsRecruiter = _context.Recruiters.Any(r => r.Email == model.Email);

                if (existsCandidate || existsRecruiter)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống.");
                    return View(model);
                }

                // Kiểm tra mật khẩu có khớp không
                if (model.Password == model.ConfirmPassword)
                {
                    // Tạo mã OTP gồm 6 số ngẫu nhiên
                    var otp = new Random().Next(100000, 999999).ToString();

                    // Lưu thông tin Recruiter với trạng thái chưa xác nhận email
                    Recruiter recruiter = new Recruiter
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        Password = HashPassword(model.Password), // Nên sử dụng hashing
                        EmailConfirmed = false,
                        OtpCode = otp, // Lưu OTP trong cơ sở dữ liệu
                        OtpExpiryTime = DateTime.Now.AddMinutes(5),// Thời gian hết hạn của OTP (5 phút)
                        Avatar = "avatar_user.png",
                        Company = model.Company,
						Gender = model.Gender
					};

                    _context.Recruiters.Add(recruiter);
                    _context.SaveChanges();

                    // Gửi OTP qua email
                    SendOtpEmail(model.Email, otp);

                    // Chuyển hướng tới trang nhập mã OTP
                    return RedirectToAction("ConfirmOtp", new { email = model.Email });
                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                }
            }

            return View(model);
        }
        public IActionResult ConfirmOtpReset(string email)
        {
            var model = new ConfirmOtpModel { Email = email };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOtpReset(ConfirmOtpModel model)
        {
            var candidate = _context.Candidates.SingleOrDefault(c => c.Email == model.Email && c.OtpCode == model.Otp);
            var recruiter = _context.Recruiters.SingleOrDefault(r => r.Email == model.Email && r.OtpCode == model.Otp);

            if (candidate != null && candidate.OtpExpiryTime > DateTime.Now)
            {
                // Xử lý cho Candidate
                candidate.OtpCode = null; // Xóa OTP sau khi xác nhận thành công
                candidate.OtpExpiryTime = null; // Xóa thông tin OTP
                _context.SaveChanges();

                // Chuyển hướng đến trang nhập lại mật khẩu
                return RedirectToAction("ResetPassword", new { email = model.Email });
            }
            else if (recruiter != null && recruiter.OtpExpiryTime > DateTime.Now)
            {
                // Xử lý cho Recruiter
                recruiter.OtpCode = null; // Xóa OTP sau khi xác nhận thành công
                recruiter.OtpExpiryTime = null; // Xóa thông tin OTP
                _context.SaveChanges();

                // Chuyển hướng đến trang nhập lại mật khẩu
                return RedirectToAction("ResetPassword", new { email = model.Email });
            }
            else
            {
                ModelState.AddModelError("Otp", "Mã OTP không đúng hoặc đã hết hạn.");
            }

            // Nếu có lỗi, trả về view với thông báo lỗi và các giá trị nhập
            return View(model); // Gửi lại model về view
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
               
                // Kiểm tra trong bảng Candidate
                var candidate = _context.Candidates.SingleOrDefault(c => c.Email == model.Email);
                if (candidate != null)
                {
                    // Tạo mã OTP ngẫu nhiên
                    var otp = new Random().Next(100000, 999999).ToString();
                    candidate.OtpCode = otp; // Gán mã OTP
                    candidate.OtpExpiryTime = DateTime.Now.AddMinutes(10); // Đặt thời gian hết hạn
                    _context.SaveChanges();

                    // Gửi email chứa mã OTP
                    SendOtpEmail(model.Email, otp);
                    return RedirectToAction("ConfirmOtpReset", new { email = model.Email });
                }

                // Kiểm tra trong bảng Recruiter
                var recruiter = _context.Recruiters.SingleOrDefault(r => r.Email == model.Email);
                if (recruiter != null)
                {
                    // Tạo mã OTP ngẫu nhiên
                    var otp = new Random().Next(100000, 999999).ToString();
                    recruiter.OtpCode = otp; // Gán mã OTP
                    recruiter.OtpExpiryTime = DateTime.Now.AddMinutes(10); // Đặt thời gian hết hạn
                    _context.SaveChanges();

                    // Gửi email chứa mã OTP
                    SendOtpEmail(model.Email, otp);
                    return RedirectToAction("ConfirmOtpReset", new { email = model.Email });
                }

                // Nếu không tìm thấy người dùng, thêm thông báo lỗi
                ModelState.AddModelError(string.Empty, "Email không tồn tại.");
            }

            // Trả lại view với thông báo lỗi
            return View(model);
        }


        public IActionResult ResetPassword(string email)
        {
            var model = new ResetPasswordModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Tìm người dùng theo email
                var candidate = _context.Candidates.SingleOrDefault(c => c.Email == model.Email);
                var recruiter = _context.Recruiters.SingleOrDefault(r => r.Email == model.Email);

                if (candidate != null)
                {
                    // Cập nhật mật khẩu cho Candidate
                    candidate.Password = HashPassword(model.Password); // Băm mật khẩu
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else if (recruiter != null)
                {
                    // Cập nhật mật khẩu cho Recruiter
                    recruiter.Password = HashPassword(model.Password); // Băm mật khẩu
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    // Nếu không tìm thấy người dùng, có thể gửi thông báo thành công giả để bảo mật
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return View(model);
        }
        public IActionResult Logout()
		{
			// Xóa tất cả session khi người dùng đăng xuất
			HttpContext.Session.Clear();
			// Hoặc xóa một session cụ thể (ví dụ email của người dùng)
			// HttpContext.Session.Remove("email");
			// Xóa cookie nếu bạn lưu trữ thông tin đăng nhập bằng cookie (nếu có)
			if (Request.Cookies["user_session"] != null)
			{
				Response.Cookies.Delete("user_session");
			}
			// Sau khi đăng xuất, chuyển hướng về trang đăng nhập hoặc trang chủ
			return RedirectToAction("Login", "Auth");
		}

        // GET: Hiển thị form thay đổi mật khẩu
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var email = HttpContext.Session.GetString("email");

            // Nếu không có email trong session, chuyển hướng đến trang đăng nhập hoặc thông báo lỗi
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Auth"); // Thay đổi tên controller nếu cần
            }

            // Tạo một đối tượng ChangePasswordModel với email từ session
            var model = new ChangePasswordModel
            {
                Email = email // Chuyển email vào model
            };

            // Trả về view ChangePassword
            return View(model);
        }

        // POST: Xử lý việc thay đổi mật khẩu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Lấy email từ session
                var email = HttpContext.Session.GetString("email");

                // Kiểm tra xem email có hợp lệ không
                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError(string.Empty, "Email không hợp lệ.");
                    return View(model);
                }

                // Kiểm tra trong bảng Candidate
                var candidate = _context.Candidates.SingleOrDefault(c => c.Email == email);
                if (candidate != null)
                {
                    // Kiểm tra mật khẩu cũ
                    if (BCrypt.Net.BCrypt.Verify(model.OldPassword, candidate.Password))
                    {
                        // Mã hóa mật khẩu mới và cập nhật vào cơ sở dữ liệu
                        candidate.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                        _context.SaveChanges();

                        // Thông báo thành công
                        TempData["success"] = "Mật khẩu của ứng viên đã được thay đổi thành công.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["error"] = "Mật khẩu cũ không chính xác.";
                        return View(model);
                    }
                }

                // Kiểm tra trong bảng Recruiter
                var recruiter = _context.Recruiters.SingleOrDefault(r => r.Email == email);
                if (recruiter != null)
                {
                    // Kiểm tra mật khẩu cũ
                    if (BCrypt.Net.BCrypt.Verify(model.OldPassword, recruiter.Password))
                    {
                        // Mã hóa mật khẩu mới và cập nhật vào cơ sở dữ liệu
                        recruiter.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                        _context.SaveChanges();

                        // Thông báo thành công
                        TempData["success"] = "Mật khẩu của nhà tuyển dụng đã được thay đổi thành công.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["error"] = "Mật khẩu cũ không chính xác.";
                        return View(model);
                    }
                }

                // Nếu không tìm thấy người dùng trong cả hai bảng
                ModelState.AddModelError(string.Empty, "Người dùng không tồn tại.");
            }

            // Nếu có lỗi, trả lại view với thông tin nhập vào
            return View(model);
        }
        private int GetRecruiterIdFromSession()
        {
            var recruiterEmail = HttpContext.Session.GetString("email");
            var recruiter = _context.Recruiters.FirstOrDefault(c => c.Email == recruiterEmail);
            return recruiter?.RecruiterId ?? 0;
        }
        public IActionResult CheckRecruiter()
        {
            int recruiterId = GetRecruiterIdFromSession(); // Get the recruiter ID from session

            if (recruiterId > 0)
            {
                // If a recruiter ID exists (greater than 0), redirect to the Create page
                return RedirectToAction("CreateJob", "Job"); // Assuming the Create action is in the Job controller
            }
            else
            {
                TempData["error"] = "Bạn phải đăng nhập với tư cách là Recruiter";
                HttpContext.Session.Clear(); // Clears the session, effectively logging the user out
                return RedirectToAction("Login", "Auth"); // Redirect to Login action in the Auth controller
            }
        }



        // Kiểm tra email qua SMTP server của Gmail



    }
}
