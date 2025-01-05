using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace C___MVC.Controllers
{
	public class ContactController : Controller
	{
		private readonly JobContext _context;
		public ContactController(JobContext context)
		{
			_context = context;	
		}
		public IActionResult Index()
		{
			return View();
		}
		private int GetCandidateIdFromSession()
		{
			var candidateEmail = HttpContext.Session.GetString("email");
			var candidate = _context.Candidates.FirstOrDefault(c => c.Email == candidateEmail);
			return candidate?.CandidateId ?? 0;
		}
		[HttpPost]
		public async Task<IActionResult> SendEmail(Contact model)
		{  
				var candidateId = GetCandidateIdFromSession();
				if (candidateId == 0)
				{
					TempData["error"] = "Please login";
					return RedirectToAction("Login", "Auth");
				}
				// Lưu Contact vào cơ sở dữ liệu
				await _context.Contacts.AddAsync(model);
				await _context.SaveChangesAsync();

				// URL encode để đảm bảo ký tự đặc biệt được xử lý đúng
				string recipientEmail = "lahuuduy28@gmail.com";
				string subject = Uri.EscapeDataString(model.Subject);
				string body = Uri.EscapeDataString(
					$"Name: {model.Name}\nEmail: {model.Email}\nTelephone: {model.Telephone}\n\nMessage: {model.Message}");

				// Tạo URL cho Gmail
				string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&to={recipientEmail}&su={subject}&body={body}";

				// Chuyển hướng tới URL
				return Redirect(gmailUrl);
		
			
		}
	}
}
