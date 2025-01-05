using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace C___MVC.Controllers
{
    public class TemplateController : Controller
    {
        private readonly JobContext jobContext;
        public IActionResult Index()
        {
            // Đường dẫn tới thư mục chứa các file template cshtml
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "Templates");

            // Lấy danh sách các file template
            var templates = Directory.GetFiles(templatePath, "*.cshtml")
                                      .Select(Path.GetFileNameWithoutExtension)
                                      .ToList();

            // Truyền danh sách các template vào view để hiển thị
            return View(templates);
        }
        // Action này sẽ trả về một view từ thư mục Templates
        public IActionResult Template(string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                return NotFound();  // Trường hợp không tìm thấy template
            }

            // Kiểm tra xem view có tồn tại không, nếu có thì trả về view từ thư mục Templates
            return View($"~/Views/Shared/Templates/{templateName}.cshtml");
        }

        private Candidate GetCandidateFromSession()
        {
            // Retrieve the candidate's email from the session
            var candidateEmail = HttpContext.Session.GetString("email");

            // Check if the email is not null or empty before querying the database
            if (string.IsNullOrEmpty(candidateEmail))
            {
                return null; // or handle accordingly (e.g., throw an exception, redirect, etc.)
            }

            // Fetch the candidate from the database using the email
            var candidate = jobContext.Candidates
                .FirstOrDefault(c => c.Email == candidateEmail);

            return candidate; // Returns null if not found
        }
        [HttpGet]
        public ActionResult CreateCV(string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                return NotFound(); // Trả về lỗi nếu không có templateName
            }

            // Bạn có thể sử dụng templateName để thực hiện logic tùy chỉnh
            ViewBag.TemplateName = templateName;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCV(CvDto cV)
        {
            // Lấy thông tin ứng viên từ session
            Candidate candidate = GetCandidateFromSession();
            if (candidate == null)
            {
                // Xử lý trường hợp không tìm thấy ứng viên
                return RedirectToAction("Login", "Auth");
            }

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

            // Thực hiện các bước lưu cvEntity vào cơ sở dữ liệu (ví dụ EF Core)

            return RedirectToAction("Index");
        }


        [HttpPost]
        private string UploadImage(IFormFile Image)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cv");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var avatarFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            var filePath = Path.Combine(uploadPath, avatarFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            return avatarFileName;
        }


    }
}
