using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelectPdf;

namespace C___MVC.Controllers
{
    public class CvController : Controller
    {
        private readonly JobContext _context;
        public CvController(JobContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> Index()
		{
			var templates = await _context.CvTemplates.ToListAsync();
			return View(templates); // Trả về View với danh sách mẫu CV
		}

		// Hiển thị mẫu CV theo ID
		//public async Task<IActionResult> ViewTemplate(int id)
		//{
		//	var template = await _context.CvTemplates.SingleOrDefaultAsync(t => t.TemplateId == id);

		//	if (template == null)
		//	{
		//		return NotFound();
		//	}
  //          // Đọc nội dung HTML từ file
  //          var htmlContent = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", template.HtmlContent.TrimStart('/')));

  //          // Trả về View với nội dung HTML và đường dẫn CSS
  //          ViewBag.HtmlContent = htmlContent;
           
		//	return View(); // Trả về View mà bạn sẽ tạo dưới đây
		//}
        private int GetCandidateIdFromSession()
        {
            var candidateEmail = HttpContext.Session.GetString("email");
            var candidate = _context.Candidates.FirstOrDefault(c => c.Email == candidateEmail);
            return candidate?.CandidateId ?? 0;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCV(int id)
        {
            int candidateId = GetCandidateIdFromSession();
            if (candidateId == 0)
            {
                TempData["error"] = "Please log in to create your CV.";
                return RedirectToAction("Login", "Auth");
            }
            // Lấy thông tin template dựa trên id
            var template = await _context.CvTemplates.SingleOrDefaultAsync(t => t.TemplateId == id);

            if (template == null)
            {
                return NotFound();
            }

            // Đọc nội dung HTML từ file
            var htmlContent = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", template.HtmlCreateContent.TrimStart('/')));

            // Truyền nội dung HTML vào ViewBag để hiển thị trong View
            ViewBag.HtmlCreateContent = htmlContent;
            TempData["TemplateId"] = template.TemplateId;

            return View(); // Hiển thị form nhập thông tin
        }
        public IActionResult ExportToPdf(int id)
        {
            // Xác định đường dẫn đầy đủ đến tệp phụ thuộc
            string depFilePath = Path.Combine(Directory.GetCurrentDirectory(), "bin\\Debug\\net8.0", "Select.Html.dep");

            // Đảm bảo rằng tệp Select.Html.dep tồn tại trong thư mục bin
            if (!System.IO.File.Exists(depFilePath))
            {
                throw new Exception("Không tìm thấy tệp Select.Html.dep trong thư mục bin.");
            }

            // Cấu hình đường dẫn tệp phụ thuộc
            AppDomain.CurrentDomain.SetData("Select.Html.dep", depFilePath);

            // Lấy thông tin template từ database
            var template = _context.CvTemplates.SingleOrDefault(t => t.TemplateId == id);
            if (template == null)
            {
                return NotFound();
            }

            // Đọc nội dung HTML từ file
            var htmlContent = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", template.HtmlCreateContent.TrimStart('/')));

            // Tạo đối tượng HtmlToPdf
            HtmlToPdf converter = new HtmlToPdf();

            // Chuyển đổi HTML thành PDF
            PdfDocument pdf = converter.ConvertHtmlString(htmlContent);

            // Lưu PDF ra file hoặc trả về dưới dạng response
            byte[] pdfBytes = pdf.Save();
            pdf.Close();

            // Trả file PDF về browser
            return File(pdfBytes, "application/pdf", "CV_Template.pdf");
        }




        [HttpPost]
        public async Task<IActionResult> CreateCV(CvDto cV)
        {
            // Lấy thông tin ứng viên từ session
            
            int candidateId = GetCandidateIdFromSession();
            if (candidateId == 0)
            {
                TempData["error"] = "Please log in to create your CV.";
                return RedirectToAction("Login", "Auth");
            }

            // Kiểm tra nếu ViewBag.TemplateId có giá trị hợp lệ
            if (TempData["TemplateId"] == null)
            {
                TempData["error"] = "Template ID is missing.";
                return RedirectToAction("Index");
            }

            int templateId = (int)TempData["TemplateId"];
            // Kiểm tra và lưu ảnh đại diện nếu có
            string profilePictureFileName = null;
            if (cV.ProfilePicture != null && cV.ProfilePicture.Length > 0)
            {
                // Đường dẫn lưu file trong wwwroot/images
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cv");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder); // Tạo thư mục nếu chưa tồn tại
                }

                // Tên file giữ nguyên
                profilePictureFileName = Path.GetFileName(cV.ProfilePicture.FileName);

                // Đường dẫn lưu file
                var filePath = Path.Combine(uploadsFolder, profilePictureFileName);

                // Lưu file lên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cV.ProfilePicture.CopyToAsync(stream);
                }
            }

            // Ánh xạ từ CvDto sang đối tượng CV
            var cvEntity = new CV
            {
                CandidateId = candidateId,
                TemplateId = templateId,
                Name = cV.Name,
                FullName = cV.FullName,
                Address = cV.Address,
                PhoneNumber = cV.PhoneNumber,
                Email = cV.Email,
                DateOfBirth = cV.DateOfBirth,
                BusinessCard = cV.BusinessCard,
                CareerObjective = cV.CareerObjective,
                ProfilePicture = profilePictureFileName, // Lưu tên file ảnh đã upload
                WorkExperience_CompanyName = cV.WorkExperience_CompanyName,
                WorkExperience_Position = cV.WorkExperience_Position,
                WorkExperience_StartDate = cV.WorkExperience_StartDate,
                WorkExperience_EndDate = cV.WorkExperience_EndDate,
                WorkExperience_Description = cV.WorkExperience_Description,
                Education_SchoolName = cV.Education_SchoolName,
                Education_Degree = cV.Education_Degree,
                Education_StartDate = cV.Education_StartDate,
                Education_EndDate = cV.Education_EndDate,
                Education_Major = cV.Education_Major,
                SkillsName = cV.SkillsName,
                Skills = cV.Skills,
                Certificate_CertificateName = cV.Certificate_CertificateName,
                Certificate_IssuedDate = cV.Certificate_IssuedDate,
                Project_ProjectName = cV.Project_ProjectName,
                Project_Description = cV.Project_Description,
                Project_Role = cV.Project_Role,
                Project_StartDate = cV.Project_StartDate,
                Project_EndDate = cV.Project_EndDate,
                Activity_ActivityName = cV.Activity_ActivityName,
                Activity_Description = cV.Activity_Description,
                Hobbies = cV.Hobbies,
                Reference_Name = cV.Reference_Name,
                Reference_Position = cV.Reference_Position,
                Reference_Company = cV.Reference_Company,
                Reference_ContactInfo = cV.Reference_ContactInfo,
                Award_AwardName = cV.Award_AwardName,
                Award_Date = cV.Award_Date
            };

            _context.CVs.Add(cvEntity); // _context là DbContext của ứng dụng
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            TempData["success"] = "Your CV has been successfully created!";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetCandidateCVs()
        {
            // Lấy CandidateId từ session
            int candidateID = GetCandidateIdFromSession();

            // Kiểm tra nếu candidateID không hợp lệ
            if (candidateID == 0)
            {
                TempData["error"] = "Candidate not logged in.";
                return RedirectToAction("Login", "Auth");
            }

            // Lấy tất cả các CV của ứng viên với CandidateId
            var candidateCVs = await _context.CVs
                .Where(cv => cv.CandidateId == candidateID)  // Lọc theo CandidateId
                .ToListAsync(); // Lấy danh sách CVs của ứng viên

            // Kiểm tra nếu không tìm thấy CVs nào
            if (candidateCVs == null || candidateCVs.Count == 0)
            {
                TempData["error"] = "No CVs found for this candidate.";
                return View(candidateCVs);
            }

            // Trả về view với danh sách các CV của ứng viên
            return View(candidateCVs);
        }


		[HttpGet]
		public async Task<IActionResult> EditCV(int id)
		{
			int candidateId = GetCandidateIdFromSession();
			if (candidateId == 0)
			{
				TempData["error"] = "Please login as candidate to create your cv.";
				return RedirectToAction("Login", "Auth");
			}

			// Lấy CV theo id và candidateId
			var cv = await _context.CVs.Include(c => c.Template)
				.Where(c => c.ID == id && c.CandidateId == candidateId)
				.SingleOrDefaultAsync();

			if (cv == null)
			{
				return NotFound();
			}

			// Tạo CvDto và gán dữ liệu từ CV vào
			var cvDto = new CvDto
			{
				CvId = cv.ID,
				CandidateId = cv.CandidateId,
				TemplateId = cv.TemplateId,
				Name = cv.Name,
				FullName = cv.FullName,
				Address = cv.Address,
				PhoneNumber = cv.PhoneNumber,
				Email = cv.Email,
				DateOfBirth = cv.DateOfBirth,
				BusinessCard = cv.BusinessCard,
				CareerObjective = cv.CareerObjective,
				ProfilePicture1 = cv.ProfilePicture, // Gán ảnh đại diện

				// Gán thông tin từ các phần khác như WorkExperience, Education, Skills, v.v.
				WorkExperience_CompanyName = cv.WorkExperience_CompanyName,
				WorkExperience_Position = cv.WorkExperience_Position,
				WorkExperience_StartDate = cv.WorkExperience_StartDate,
				WorkExperience_EndDate = cv.WorkExperience_EndDate,
				WorkExperience_Description = cv.WorkExperience_Description,

				Education_SchoolName = cv.Education_SchoolName,
				Education_Degree = cv.Education_Degree,
				Education_StartDate = cv.Education_StartDate,
				Education_EndDate = cv.Education_EndDate,
				Education_Major = cv.Education_Major,

				SkillsName = cv.SkillsName,
				Skills = cv.Skills,

				Certificate_CertificateName = cv.Certificate_CertificateName,
				Certificate_IssuedDate = cv.Certificate_IssuedDate,

				Project_ProjectName = cv.Project_ProjectName,
				Project_Description = cv.Project_Description,
				Project_Role = cv.Project_Role,
				Project_StartDate = cv.Project_StartDate,
				Project_EndDate = cv.Project_EndDate,

				Activity_ActivityName = cv.Activity_ActivityName,
				Activity_Description = cv.Activity_Description,

				Hobbies = cv.Hobbies,

				Reference_Name = cv.Reference_Name,
				Reference_Position = cv.Reference_Position,
				Reference_Company = cv.Reference_Company,
				Reference_ContactInfo = cv.Reference_ContactInfo,

				Award_AwardName = cv.Award_AwardName,
				Award_Date = cv.Award_Date
			};

			return View(cvDto);
		}





		[HttpPost]
        public async Task<IActionResult> EditCV(CvDto cV)
        {
            int candidateID = GetCandidateIdFromSession();

            // Kiểm tra nếu candidateID không hợp lệ
            if (candidateID == 0)
            {
                TempData["error"] = "Please login as candidate to create your cv.";
                return RedirectToAction("Login", "Auth");
            }

            // Lấy CV từ cơ sở dữ liệu
            var cvEntity = await _context.CVs
                .Where(cv => cv.ID == cV.CvId && cv.CandidateId == candidateID)
                .SingleOrDefaultAsync();

            if (cvEntity == null)
            {
                TempData["error"] = "CV not found or you are not authorized to edit this CV.";
                return RedirectToAction("GetCandidateCVs");
            }

            // Kiểm tra và lưu ảnh đại diện nếu có
            string profilePictureFileName = null;
            if (cV.ProfilePicture != null && cV.ProfilePicture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cv");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                profilePictureFileName = Path.GetFileName(cV.ProfilePicture.FileName);
                var filePath = Path.Combine(uploadsFolder, profilePictureFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cV.ProfilePicture.CopyToAsync(stream);
                }
            }


            cvEntity.Name = cV.Name;
            cvEntity.FullName = cV.FullName;
            cvEntity.Address = cV.Address;
            cvEntity.PhoneNumber = cV.PhoneNumber;
            cvEntity.Email = cV.Email;
            cvEntity.DateOfBirth = cV.DateOfBirth;
            cvEntity.BusinessCard = cV.BusinessCard;
            cvEntity.CareerObjective = cV.CareerObjective;
            cvEntity.ProfilePicture = profilePictureFileName ?? cvEntity.ProfilePicture; // Cập nhật ảnh nếu có

            // Cập nhật các trường thông tin khác (WorkExperience, Education, Skills, etc.)
            cvEntity.WorkExperience_CompanyName = cV.WorkExperience_CompanyName;
            cvEntity.WorkExperience_Position = cV.WorkExperience_Position;
            cvEntity.WorkExperience_StartDate = cV.WorkExperience_StartDate;
            cvEntity.WorkExperience_EndDate = cV.WorkExperience_EndDate;
            cvEntity.WorkExperience_Description = cV.WorkExperience_Description;

            cvEntity.Education_SchoolName = cV.Education_SchoolName;
            cvEntity.Education_Degree = cV.Education_Degree;
            cvEntity.Education_StartDate = cV.Education_StartDate;
            cvEntity.Education_EndDate = cV.Education_EndDate;
            cvEntity.Education_Major = cV.Education_Major;

            cvEntity.SkillsName = cV.SkillsName;
            cvEntity.Skills = cV.Skills;

            cvEntity.Certificate_CertificateName = cV.Certificate_CertificateName;
            cvEntity.Certificate_IssuedDate = cV.Certificate_IssuedDate;

            cvEntity.Project_ProjectName = cV.Project_ProjectName;
            cvEntity.Project_Description = cV.Project_Description;
            cvEntity.Project_Role = cV.Project_Role;
            cvEntity.Project_StartDate = cV.Project_StartDate;
            cvEntity.Project_EndDate = cV.Project_EndDate;

            cvEntity.Activity_ActivityName = cV.Activity_ActivityName;
            cvEntity.Activity_Description = cV.Activity_Description;

            cvEntity.Hobbies = cV.Hobbies;

            cvEntity.Reference_Name = cV.Reference_Name;
            cvEntity.Reference_Position = cV.Reference_Position;
            cvEntity.Reference_Company = cV.Reference_Company;
            cvEntity.Reference_ContactInfo = cV.Reference_ContactInfo;

            cvEntity.Award_AwardName = cV.Award_AwardName;
            cvEntity.Award_Date = cV.Award_Date;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.CVs.Update(cvEntity);
            await _context.SaveChangesAsync();

            TempData["success"] = "Your CV has been successfully updated!";
            return RedirectToAction("GetCandidateCVs");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteCV(int id)
		{
			// Tìm CV trong cơ sở dữ liệu
			var cv = await _context.CVs.FindAsync(id);
			if (cv == null)
			{
				return NotFound();  // Nếu không tìm thấy CV, trả về lỗi 404
			}

			// Xóa CV
			_context.CVs.Remove(cv);
			await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            TempData["success"] = "Your CV has been deleted successfully.";

            // Chuyển hướng về trang danh sách CVs sau khi xóa thành công
            return RedirectToAction("GetCandidateCVs", "Cv");  // Giả sử bạn có action Index để hiển thị danh sách CV
		}
	}
}
