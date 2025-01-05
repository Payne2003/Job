using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace C___MVC.Controllers
{
    public class CandidateCVController : Controller
    {
        private readonly JobContext jobContext;
        public CandidateCVController (JobContext jobContext)
        {

            this.jobContext = jobContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpLoadCv()
        {
            return View();
        }
        //[HttpPost]
  //      public async Task<IActionResult> UpLoadCv(CvDto model)
  //      {
  //          int candidate_Id = GetCandidateIdFromSession();

  //          // Kiểm tra nếu candidate_Id hợp lệ
  //          if (candidate_Id == 0)
  //          {
  //              TempData["error"] = "Hãy đăng nhập với tư cách candidate";
  //              return RedirectToAction("Login", "Auth");
  //          }
  //          else
  //          {
  //              string cvFilePath = null;

  //              if (model.Cv.Length > 0)
  //              {
  //                  var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.Cv.FileName);
  //                  using (var stream = new FileStream(filePath, FileMode.Create))
  //                  {
  //                      await model.Cv.CopyToAsync(stream);
  //                  }
  //                  cvFilePath = model.Cv.FileName;
  //              }

  //              // Tạo đối tượng CV và lưu vào database
  //              var CV = new CV
  //              {
  //                  Name = cvFilePath
  //              };

  //              jobContext.CVs.Add(CV);
  //              await jobContext.SaveChangesAsync(); // Lưu CV và lấy CVId

  //              // Sau khi có CVId, tạo đối tượng CandidateCV và lưu vào bảng CandidateCV
  //              var candidateCV = new CandidateCV
  //              {
  //                  CandidateId = candidate_Id, // CandidateId lấy từ session
  //                  CVId = CV.ID // CVId của CV vừa thêm
  //              };

  //              jobContext.CandidateCVs.Add(candidateCV);
  //              await jobContext.SaveChangesAsync(); // Lưu CandidateCV

  //              TempData["success"] = "CV đã được tải lên thành công!";
  //              return View();
  //          }
  //      }

       
  //      private int GetCandidateIdFromSession()
  //      {
  //          var candidateEmail = HttpContext.Session.GetString("email");
  //          var candidate = jobContext.Candidates.FirstOrDefault(c => c.Email == candidateEmail);
  //          return candidate?.CandidateId ?? 0;
  //      }

  //      private Candidate GetCandidateFromSession()
  //      {
  //          // Retrieve the candidate's email from the session
  //          var candidateEmail = HttpContext.Session.GetString("email");

  //          // Check if the email is not null or empty before querying the database
  //          if (string.IsNullOrEmpty(candidateEmail))
  //          {
  //              return null; // or handle accordingly (e.g., throw an exception, redirect, etc.)
  //          }

  //          // Fetch the candidate from the database using the email
  //          var candidate = jobContext.Candidates
  //              .FirstOrDefault(c => c.Email == candidateEmail);

  //          return candidate; // Returns null if not found
  //      }

		//[HttpGet]
		//public async Task<IActionResult> GetCandidateCVs()
		//{
		//	int candidateID = GetCandidateIdFromSession();

		//	// Lấy tất cả các CV liên kết với ứng viên thông qua CandidateCV
		//	var candidateCVs = await jobContext.CandidateCVs
		//		.Where(cc => cc.CandidateId == candidateID)   // Lọc theo CandidateId
		//		.Include(cc => cc.CV)                         // Bao gồm cả CV
		//		.ToListAsync();                               // Lấy danh sách CandidateCV

		//	if (candidateCVs == null || candidateCVs.Count == 0)
		//	{
		//		return NotFound("No CVs found for this candidate.");
		//	}

		//	var cvs = candidateCVs.Select(cc => cc.CV).ToList();

		//	return View(candidateCVs); 
		//}

       
    }
}
