using Azure;
using C___MVC.Data;
using C___MVC.Dto;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace C___MVC.Controllers
{
    public class JobController : Controller
    {
        private readonly JobContext _context;
        public JobController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id,int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            int candidateId = GetCandidateIdFromSession();
            var lstJob = _context.Jobs.Include(l => l.Links)
                .Include(j => j.Category)
                .Include(j => j.Recruiter)
                .Include(j => j.Location)
                .Include(j => j.Skills)
                .Include(l => l.Skills).AsNoTracking().OrderBy(x => x.JobId);

            // Áp dụng điều kiện lọc nếu id được cung cấp
            if (id.HasValue && id.Value > 0)
            {
                lstJob = (IOrderedQueryable<Job>)lstJob.Where(j => j.CategoryId == id.Value);
            }

            // Thêm điều kiện để chỉ lấy công việc có ngày hết hạn lớn hơn ngày hiện tại
            lstJob = (IOrderedQueryable<Job>)lstJob.Where(j => j.ExpiredAt > DateTime.Now);

            // Thực hiện truy vấn và lấy kết quả
            var jobs = lstJob
                .Where(j => j.Status == true) // Điều kiện lọc theo Status
				.ToPagedList(pageNumber, pageSize);

            // Lấy danh sách ID của các Job đã lưu
            var savedJobIds = _context.SavedJobs
                .Where(s => s.CandidateId == candidateId)
                .Select(s => s.JobId)
                .ToList();

            // Thêm danh sách ID vào ViewBag để sử dụng trong View
            ViewBag.SavedJobIds = savedJobIds;
			
			// Trả về view với danh sách công việc
			return View(jobs);
        }


        [HttpPost]
        public async Task<IActionResult> FilterBySkills(List<int> skillIds, int? page)
        {
            int candidateId = GetCandidateIdFromSession();
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			// Nếu không có kỹ năng nào được chọn, trả về danh sách công việc không lọc
			if (skillIds == null || !skillIds.Any())
            {
                return RedirectToAction("Index");
            }
            // Lọc công việc dựa trên kỹ năng được chọn
            var jobs = _context.Jobs
                .Where(j => j.Skills.Any(s => skillIds.Contains(s.SkillId)) && j.Status == true)
                .Include(j => j.Category)
                .Include(j => j.Recruiter)
                .Include(j => j.Location)
                .Include(j => j.Links)
                .Include(j => j.Skills)
                .ToPagedList(pageNumber,pageSize);

            var savedJobIds = _context.SavedJobs
      .Where(s => s.CandidateId == candidateId)
      .Select(s => s.JobId)
      .ToList();

            // Thêm danh sách ID vào ViewBag để sử dụng trong View
            ViewBag.SavedJobIds = savedJobIds;
            return PartialView("FilterBySkills", jobs);// Trả về view Index với danh sách công việc lọc
        }
        [HttpGet]
		public IActionResult FindJob(string key, int? locationId, int? categoryId,int? page)
		{
            int candidateId = GetCandidateIdFromSession();
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			// Khởi tạo truy vấn
			var query = _context.Jobs.AsQueryable();

			// Tìm kiếm theo từ khóa
			if (!string.IsNullOrEmpty(key))
			{
				query = query.Where(j => j.Title.Contains(key));
			}

			// Tìm kiếm theo địa điểm
			if (locationId.HasValue)
			{
				query = query.Where(j => j.LocationId == locationId.Value);
			}

			// Tìm kiếm theo danh mục
			if (categoryId.HasValue)
			{
				query = query.Where(j => j.CategoryId == categoryId.Value);
			}

			// Lọc các job có Status là true
			query = query.Where(j => j.Status == true);

			// Lấy danh sách kết quả tìm kiếm với các thuộc tính liên kết
			var jobs = query
				.Include(j => j.Location)
				.Include(j => j.Category)
                .Include(j => j.Location)
                .Include(j => j.Links)
                .Include(j => j.Skills)
                .ToList().ToPagedList(pageNumber, pageSize);

            var savedJobIds = _context.SavedJobs
        .Where(s => s.CandidateId == candidateId)
        .Select(s => s.JobId)
        .ToList();

            // Thêm danh sách ID vào ViewBag để sử dụng trong View
            ViewBag.SavedJobIds = savedJobIds;

            return View(jobs);
        }
        [HttpPost]
		public IActionResult FindJobNotKey(int? locationId, int? categoryId,int? page)
		{
            int candidateId = GetCandidateIdFromSession();
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			// Khởi tạo truy vấn
			var query = _context.Jobs.AsQueryable();
			// Tìm kiếm theo địa điểm
			if (locationId.HasValue)
			{
				query = query.Where(j => j.LocationId == locationId.Value);
			}

			// Tìm kiếm theo danh mục
			if (categoryId.HasValue)
			{
				query = query.Where(j => j.CategoryId == categoryId.Value);
			}

			// Lọc các job có Status là true
			query = query.Where(j => j.Status == true);

			// Lấy danh sách kết quả tìm kiếm với các thuộc tính liên kết
			var jobs = query
				.Include(j => j.Location)
				.Include(j => j.Category)
                .Include(j => j.Location)
                .Include(j => j.Links)
                .Include(j => j.Skills)
				.ToPagedList(pageNumber, pageSize);

            var savedJobIds = _context.SavedJobs
            .Where(s => s.CandidateId == candidateId)
            .Select(s => s.JobId)
            .ToList();

            // Thêm danh sách ID vào ViewBag để sử dụng trong View
            ViewBag.SavedJobIds = savedJobIds;

            return PartialView("FindJobNotKey", jobs);

        }
		[HttpPost]
		public IActionResult FindJobNotKey_ByRecruiter(int? locationId, int? categoryId, int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			// Khởi tạo truy vấn
			var query = _context.Jobs.AsQueryable();
			// Tìm kiếm theo địa điểm
			if (locationId.HasValue)
			{
				query = query.Where(j => j.LocationId == locationId.Value);
			}

			// Tìm kiếm theo danh mục
			if (categoryId.HasValue)
			{
				query = query.Where(j => j.CategoryId == categoryId.Value);
			}

			// Lọc các job có Status là true
			query = query.Where(j => j.Status == true);

			int recruiterId = GetRecruiterIdFromSession();

			if (recruiterId <= 0)
			{
				return NotFound();
			}

			var job = query
				.Where(j => j.RecruiterId == recruiterId)
				.Include(j => j.Category) // Bao gồm Category nếu cần hiển thị
				.Include(j => j.Recruiter)
				.Include(j => j.Location)
				.Include(j => j.Links)    // Bao gồm thông tin về Link
				.Include(j => j.Skills) // Nếu bạn muốn hiển thị thông tin về công việc
				.ToPagedList(pageNumber, pageSize);

			if (job == null)
			{
				return NotFound();
			}
			var JobRecruiterName = _context.Jobs
				.Where(j => j.RecruiterId == recruiterId)
				.Select(j => j.Recruiter.UserName)
				.FirstOrDefault();

			ViewBag.RecruiterName = JobRecruiterName;

			return PartialView("FindJobNotKey_ByRecruiter", job);

		}
        [HttpPost]
        public IActionResult FindJobSavedNotKey(int? locationId, int? categoryId )
        {
            // Lấy ID ứng viên từ session
            int candidateId = GetCandidateIdFromSession();
			
			// Lấy danh sách các công việc đã lưu của ứng viên
			var savedJobs = _context.SavedJobs
                .Include(s => s.Job)
                .ThenInclude(j => j.Location) // Bao gồm thông tin địa điểm của công việc
                .Include(s => s.Job)
                .ThenInclude(j => j.Links)    // Bao gồm thông tin liên kết của công việc
                .Include(s => s.Job)
                .ThenInclude(j => j.Category) // Bao gồm thông tin danh mục của công việc
                .Where(s => s.CandidateId == candidateId);

            // Nếu lọc theo địa điểm
            if (locationId.HasValue)
            {
                savedJobs = savedJobs.Where(s => s.Job.LocationId == locationId.Value);
            }

            // Nếu lọc theo danh mục
            if (categoryId.HasValue)
            {
                savedJobs = savedJobs.Where(s => s.Job.CategoryId == categoryId.Value);
            }

            // Chỉ lấy các công việc còn hoạt động
            var result = savedJobs
                .Where(s => s.Job.Status == true)
                .ToList();

            // Trả về PartialView với danh sách công việc đã lưu
            return PartialView("FindJobSavedNotKey", result);
        }



        public IActionResult CreateJob()
        {
            LoadDropdownData();
            return View();
        }

		[HttpPost]
		public IActionResult CreateJob(JobRequestDto dto, IFormFile Image, List<Link> Links, List<Skill> Skills)
		{
			// Lấy RecruiterId từ session
			int recruiterid = GetRecruiterIdFromSession();

			if (recruiterid == null)
			{
				ModelState.AddModelError("", "Recruiter not found.");
				LoadDropdownData();
				return View(dto);
			}

			// Kiểm tra Category và Location
			if (!_context.Categories.Any(c => c.CategoryId == dto.CategoryId))
			{
				ModelState.AddModelError("CategoryId", "Selected category does not exist.");
				LoadDropdownData();
				return View(dto);
			}
			if (dto.ExpiredAt <= DateTime.Now)
			{
				ModelState.AddModelError("ExpiredAt", "The expiration date must be in the future.");
				LoadDropdownData();
				return View(dto);
			}

			if (!_context.Locations.Any(l => l.LocationId == dto.LocationId))
			{
				ModelState.AddModelError("LocationId", "Selected location does not exist.");
				LoadDropdownData();
				return View(dto);
			}

			// Xử lý upload ảnh
			string avatarFileName = "default.jpg"; // Giá trị mặc định nếu không upload ảnh
			if (Image != null && Image.Length > 0)
			{
				avatarFileName = UploadImage(Image);
			}

			// Tạo Job và lưu vào cơ sở dữ liệu
			var job = new Job
			{
				Title = dto.Title,
				Description = dto.Description,
				Experience = dto.Experience,
				Company = dto.Company,
				Quantity = dto.Quantity,
				LocationId = dto.LocationId,
				Salary = dto.Salary,
				CategoryId = dto.CategoryId,
				ExpiredAt = dto.ExpiredAt,
				CreatedAt = DateTime.Now,
				Image = avatarFileName,
				RecruiterId = recruiterid,
			};

			_context.Jobs.Add(job);
			_context.SaveChanges(); // Lưu Job và lấy JobId

			// Thêm liên kết (links) nếu có
			if (Links != null && Links.Any())
			{
				AddLinksToJob(job.JobId, Links);
			}

			// Thêm kỹ năng (skills) nếu có
			if (Skills != null && Skills.Any())
			{
				AddSkillsToJob(job.JobId, Skills);
			}

			TempData["success"] = "Job added successfully with links and skills!";
			return RedirectToAction("Index", "Job");
		}
        public IActionResult EditJob(int id)
        {
            // Lấy Job từ cơ sở dữ liệu theo id
            var job = _context.Jobs.Include(j => j.Links).Include(j => j.Skills).FirstOrDefault(j => j.JobId == id);

            if (job == null)
            {
                return NotFound();
            }

            // Chuyển đổi Job sang DTO để hiển thị lên View
            var dto = new JobRequestDto
            {
                Title = job.Title,
                Description = job.Description,
                Experience = job.Experience,
                Company = job.Company,
                Quantity = job.Quantity,
                LocationId = job.LocationId,
                Salary = job.Salary,
                CategoryId = job.CategoryId,
                ExpiredAt = job.ExpiredAt,
                Image = job.Image,
            };

            ViewBag.Links = job.Links ?? new List<Link>();
            ViewBag.Skills = job.Skills ?? new List<Skill>();
            LoadDropdownData(); // Load Category và Location dropdown

            return View(dto);
        }

        [HttpPost]
        public IActionResult EditJob(int id, JobRequestDto dto, IFormFile Image, List<Link> Links, List<Skill> Skills)
        {
            // Lấy Job từ cơ sở dữ liệu
            var job = _context.Jobs.Include(j => j.Links).Include(j => j.Skills).FirstOrDefault(j => j.JobId == id);

            if (job == null)
            {
                return NotFound();
            }

            // Kiểm tra Category và Location
            if (!_context.Categories.Any(c => c.CategoryId == dto.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Selected category does not exist.");
                LoadDropdownData();
                return View(dto);
            }

            if (dto.ExpiredAt <= DateTime.Now)
            {
                ModelState.AddModelError("ExpiredAt", "The expiration date must be in the future.");
                LoadDropdownData();
                return View(dto);
            }

            if (!_context.Locations.Any(l => l.LocationId == dto.LocationId))
            {
                ModelState.AddModelError("LocationId", "Selected location does not exist.");
                LoadDropdownData();
                return View(dto);
            }

            // Xử lý upload ảnh
            if (Image != null && Image.Length > 0)
            {
                var avatarFileName = UploadImage(Image);
                job.Image = avatarFileName; // Cập nhật hình ảnh nếu có
            }

            // Cập nhật các thuộc tính của Job
            job.Title = dto.Title;
            job.Description = dto.Description;
            job.Experience = dto.Experience;
            job.Company = dto.Company;
            job.Quantity = dto.Quantity;
            job.LocationId = dto.LocationId;
            job.Salary = dto.Salary;
            job.CategoryId = dto.CategoryId;
            job.ExpiredAt = dto.ExpiredAt;



            // Cập nhật Links
            if (Links != null && Links.Any())
            {
                // Xóa các Links hiện tại
                _context.Links.RemoveRange(job.Links);

                // Thêm Links mới
                AddLinksToJob(job.JobId, Links);
            }

            // Cập nhật Skills
            if (Skills != null && Skills.Any())
            {
                // Xóa các Skills hiện tại
                _context.Skills.RemoveRange(job.Skills);

                // Thêm Skills mới
                AddSkillsToJob(job.JobId, Skills);
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            TempData["success"] = "Job updated successfully!";
            return RedirectToAction("ListByRecruiter", "Resume");
        }
		[HttpPost]
		public IActionResult Delete(int id)
		{
			// Tìm Job theo id
			var jobRequest = _context.Jobs
				.Include(j => j.Links)
				.Include(j => j.Skills)
				.Include(j => j.Resumes)
				.FirstOrDefault(j => j.JobId == id);

			if (jobRequest == null)
			{
				TempData["error"] = "Job not found.";
				return RedirectToAction("Index"); // Trả về trang danh sách công việc
			}

			// Xóa các liên kết (Links) liên quan đến Job
			if (jobRequest.Links != null && jobRequest.Links.Any())
			{
				_context.Links.RemoveRange(jobRequest.Links);
			}

			// Xóa các kỹ năng (Skills) liên quan đến Job
			if (jobRequest.Skills != null && jobRequest.Skills.Any())
			{
				_context.Skills.RemoveRange(jobRequest.Skills);
			}

			// Xóa các hồ sơ ứng tuyển (Resumes) liên quan đến Job
			if (jobRequest.Resumes != null && jobRequest.Resumes.Any())
			{
				_context.Resumes.RemoveRange(jobRequest.Resumes);
			}

			// Xóa Job
			_context.Jobs.Remove(jobRequest);
			_context.SaveChanges();

			TempData["success"] = "Job deleted successfully.";
			return RedirectToAction("ListByRecruiter", "Resume"); // Quay lại trang danh sách công việc
		}

		private void AddLinksToJob(int jobId, List<Link> links)
        {
            foreach (var link in links)
            {
                if (!string.IsNullOrEmpty(link.Name) && !string.IsNullOrEmpty(link.Description))
                {
                    link.JobId = jobId; // Gán JobId cho Link
                    _context.Links.Add(link);
                }
            }
            _context.SaveChanges(); // Lưu các Link vào cơ sở dữ liệu
        }
        private void AddSkillsToJob(int jobId, List<Skill> Skills)
        {
            foreach (var skill in Skills)
            {
                if (!string.IsNullOrEmpty(skill.Name))
                {
                    skill.JobId = jobId; // Gán JobId cho Link
                    _context.Skills.Add(skill);
                }
            }
            _context.SaveChanges(); // Lưu các Link vào cơ sở dữ liệu
        }

        private void LoadDropdownData()
        {
            ViewBag.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();
            ViewBag.Locations = _context.Locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.Province
            }).ToList();
        }
        private string UploadImage(IFormFile Image)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

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

        public IActionResult JobByCategory(int? id,int? page)
        {
            int candidateId = GetCandidateIdFromSession();
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			var jobs = _context.Jobs
                   .Where(j => j.CategoryId == id && j.Status == true) // Lọc theo CategoryId và Status
                   .Include(j => j.Category)    // Bao gồm thông tin về Category
                   .Include(j => j.Recruiter)   // Bao gồm thông tin về Recruiter
                   .Include(j => j.Location)    // Bao gồm thông tin về Location
                   .Include(j => j.Links)    // Bao gồm thông tin về Link
                   .Include(j => j.Skills)      // Bao gồm thông tin về Skills
				   .ToPagedList(pageNumber, pageSize);
            var savedJobIds = _context.SavedJobs
           .Where(s => s.CandidateId == candidateId)
           .Select(s => s.JobId)
           .ToList();

            // Thêm danh sách ID vào ViewBag để sử dụng trong View
            ViewBag.SavedJobIds = savedJobIds;

            return PartialView("JobByCategory", jobs);
        }
		public IActionResult JobByCategory1(int? id, int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			int candidateId = GetCandidateIdFromSession();
			var jobs = _context.Jobs
				   .Where(j => j.CategoryId == id && j.Status == true) // Lọc theo CategoryId và Status
				   .Include(j => j.Category)    // Bao gồm thông tin về Category
				   .Include(j => j.Recruiter)   // Bao gồm thông tin về Recruiter
				   .Include(j => j.Location)    // Bao gồm thông tin về Location
				   .Include(j => j.Links)    // Bao gồm thông tin về Link
				   .Include(j => j.Skills)      // Bao gồm thông tin về Skills
					.ToPagedList(pageNumber, pageSize);
			var savedJobIds = _context.SavedJobs
		   .Where(s => s.CandidateId == candidateId)
		   .Select(s => s.JobId)
		   .ToList();

			// Thêm danh sách ID vào ViewBag để sử dụng trong View
			ViewBag.SavedJobIds = savedJobIds;

			return PartialView("JobByCategory1", jobs);
		}

        public IActionResult Details(int id)
        {
            int candidateId = GetCandidateIdFromSession();

            // Lấy Job chi tiết bao gồm các thông tin liên quan
            var job = _context.Jobs
                .Include(j => j.Category)  // Bao gồm thông tin Category
                .Include(j => j.Recruiter)
                .Include(j => j.Links)     // Bao gồm thông tin về Links
                .Include(j => j.Location)  // Bao gồm thông tin Location
                .Include(j => j.Skills)    // Bao gồm thông tin Skills
                .FirstOrDefault(j => j.JobId == id);

            // Nếu không tìm thấy công việc, trả về NotFound
            if (job == null)
            {
                return NotFound();
            }

            // Tính số ngày còn lại
            var daysLeft = (job.ExpiredAt - DateTime.Now).Days;
            ViewBag.DaysLeft = daysLeft;

            // Lấy danh sách công việc đã lưu của người dùng
            var savedJobIds = _context.SavedJobs
                .Where(s => s.CandidateId == candidateId)
                .Select(s => s.JobId)
                .ToList();

            // Kiểm tra công việc hiện tại đã được lưu chưa
            bool isSaved = savedJobIds.Contains(id);
            ViewBag.IsSaved = isSaved;
            ViewBag.CandidateId = candidateId;

            // **Lấy danh sách công việc liên quan**
            var relatedJobs = _context.Jobs
               .Include(j => j.Category)  // Bao gồm thông tin Category
                .Include(j => j.Recruiter)
                .Include(j => j.Links)     // Bao gồm thông tin về Links
                .Include(j => j.Location)  // Bao gồm thông tin Location
                .Include(j => j.Skills)
                .Where(j => j.JobId != id &&
                            j.CategoryId == job.CategoryId  // Cùng Category
                            )        // Chỉ lấy công việc chưa hết hạn
                .Take(2) // Giới hạn số lượng công việc liên quan (ví dụ: 5 công việc)
                .ToList();

            // Truyền danh sách công việc liên quan sang View
            ViewBag.RelatedJobs = relatedJobs;

            // Trả về view với thông tin công việc và danh sách liên quan
            return View(job);
        }

        private int GetRecruiterIdFromSession()
		{
			var recruiterEmail = HttpContext.Session.GetString("email");
			var recruiter = _context.Recruiters.FirstOrDefault(c => c.Email == recruiterEmail);
			return recruiter?.RecruiterId ?? 0;
		}
		private int GetCandidateIdFromSession()
		{
			var candidateEmail = HttpContext.Session.GetString("email");
			var candidate = _context.Candidates.FirstOrDefault(c => c.Email == candidateEmail);
			return candidate?.CandidateId ?? 0;
		}
        public IActionResult SavedJob()
        {
            // Lấy CandidateId từ session (giả sử bạn đã lưu CandidateId trong session khi người dùng đăng nhập)
            var candidateId = GetCandidateIdFromSession();

            // Kiểm tra xem CandidateId có hợp lệ không
            if (candidateId == null)
            {
                // Nếu không có CandidateId trong session, chuyển hướng đến trang đăng nhập hoặc xử lý lỗi
                return RedirectToAction("Login", "Auth");
            }

            // Lấy danh sách công việc đã lưu cho candidate cụ thể
            var savedJobs = _context.SavedJobs
                .Where(s => s.CandidateId == candidateId)  // Lọc theo CandidateId
                .Include(s => s.Job)
                .ThenInclude(j => j.Location)
                .ToList();

            // Nếu không có công việc nào được lưu
            if (!savedJobs.Any())
            {
                // Có thể trả về một view khác hoặc hiển thị thông báo không có công việc đã lưu
                ViewBag.Message = "Bạn chưa lưu công việc nào.";
                return View(new List<SavedJob>());
            }

            // Trả về view với danh sách công việc đã lưu
            return View(savedJobs);
        }




        [HttpPost]
        public async Task<IActionResult> SaveJob(int jobId)
        {
            int candidateId = GetCandidateIdFromSession();

            if (candidateId == 0) // Kiểm tra xem người dùng đã đăng nhập chưa
            {
                return Json(new { success = false, message = "Please log in to save jobs." });
            }

            // Kiểm tra xem công việc đã được lưu trước đó chưa
            var existingSavedJob = await _context.SavedJobs
                .FirstOrDefaultAsync(s => s.JobId == jobId && s.CandidateId == candidateId);
            if (existingSavedJob != null)
            {
                // Nếu công việc đã được lưu, thực hiện xóa công việc đó
                _context.SavedJobs.Remove(existingSavedJob);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Job has been removed from saved jobs." });
            }
            else
            {
                // Nếu công việc chưa được lưu, thực hiện lưu công việc
                var savedJob = new SavedJob
                {
                    JobId = jobId,
                    CandidateId = candidateId,
                    SavedAt = DateTime.Now // Lưu thời gian hiện tại
                };

                _context.SavedJobs.Add(savedJob);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Job has been saved successfully!" });
            }
        }

        [HttpPost]
        public IActionResult DeleteSavedJob(int jobId)
        {
            var candidateId = GetCandidateIdFromSession(); // Giả sử có hàm để lấy candidate hiện tại

            if (candidateId == null)
            {
                return Json(new { success = false, message = "User not logged in." });
            }

            var savedJob = _context.SavedJobs
                .FirstOrDefault(sj => sj.JobId == jobId && sj.CandidateId == candidateId);

            if (savedJob != null)
            {
                _context.SavedJobs.Remove(savedJob);
                _context.SaveChanges();
                return Json(new { success = true, message = "Job has been deleted successfully!" });
            }

            return Json(new { success = false, message = "Saved job not found!" });
        }


        [HttpPost]
        public IActionResult ShowMessage(int id)
        {
            // Đặt thông báo vào TempData
            TempData["error"] = "The job has expired.";

            // Chuyển hướng về trang Details của Job với ID đã truyền vào
            return RedirectToAction("Details", "Job", new { id = id });
        }
        [HttpPost]
        public IActionResult ShowMessage1()
        {
            // Đặt thông báo vào TempData
            TempData["error"] = "Please login as candidate.";

            // Chuyển hướng về trang Details của Job với ID đã truyền vào
            return RedirectToAction("Login", "Auth");
        }



    }
}
