using Azure;
using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace C___MVC.Controllers
{
    public class JobmanagerController : Controller
    {
        private readonly JobContext _context;
        public JobmanagerController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstJob = _context.Jobs
                 .Include(j => j.Recruiter)
                 .Include(j => j.Category)
                 .Include(j => j.Location)
                 .AsNoTracking()
                 .Where(j => j.Status == true) // Chỉ lấy các Job đã được chấp nhận
                 .OrderBy(x => x.JobId);

            PagedList<Job> lst = new PagedList<Job>(lstJob, pageNumber, pageSize);
            return View(lst);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var job = _context.Jobs.Include(j => j.Links).Include(j => j.Skills).Include(r => r.Resumes).FirstOrDefault(j => j.JobId == id);
            if (job == null)
            {
                TempData["error"] = "Job not found."; // Thêm thông báo lỗi nếu không tìm thấy Job
                return Json(new { success = false });
            }
            // Xóa các liên kết (Links) liên quan đến Recruiter
            if (job.Links != null && job.Links.Any())
            {
                _context.Links.RemoveRange(job.Links);
            }

            // Xóa các kỹ năng (Skills) liên quan đến Recruiter
            if (job.Skills != null && job.Skills.Any())
            {
                _context.Skills.RemoveRange(job.Skills);
            }
            // Xóa các kỹ năng (Skills) liên quan đến Recruiter
            if (job.Resumes != null && job.Resumes.Any())
            {
                _context.Resumes.RemoveRange(job.Resumes);
            }
            // Xóa Job, Links và Skills sẽ được xóa tự động
            _context.Jobs.Remove(job);
            _context.SaveChanges();

            TempData["success"] = "Job deleted successfully!"; // Thêm thông báo thành công
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> GetJobDetails([FromBody] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Job ID.");
            }

            var job = await _context.Jobs
                .Include(j => j.Recruiter)  // Load Recruiter
                .Include(j => j.Category)   // Load Category
                .Include(j => j.Location)
                .Include(j => j.Links)      // Load Links
                .Include(j => j.Skills)      // Load Links
                .Where(j => j.JobId == id)
                .Select(j => new
                {
                    j.JobId,                    // Bao gồm JobId
                    j.Title,
                    j.Description,
                    j.Experience,
                    j.Company,
                    j.Quantity,
                    Location = j.Location.Province,
                    j.Salary,
                    RecruiterEmail = j.Recruiter.Email,  // Truy xuất thông tin từ Recruiter
                    CategoryName = j.Category.Name,      // Truy xuất thông tin từ Category
                    j.Image,
                    Links = j.Links.Select(l => new
                    {
                        l.Description,
                        l.Name
                    }),
                    Skills = j.Skills.Select(s => new
                    {
                        s.Description,
                        s.Name
                    })
                }).FirstOrDefaultAsync();

            if (job == null)
            {
                return NotFound();
            }

            return Json(job);  // Trả về dữ liệu JSON với đầy đủ thông tin
        }

        [HttpGet]
        public IActionResult Search(string query, int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Xử lý query null hoặc chuỗi trắng
            query = query?.Trim() ?? "";

            // Truy vấn với điều kiện lọc và phân trang
            var lstjobrequest = _context.Jobs
                .Where(j => j.Status == true && // Chỉ lấy các Job có Status là false
                            (string.IsNullOrEmpty(query)
                            || j.Title.Contains(query)
                            || j.Company.Contains(query)
                            || j.Location.Province.Contains(query))) // So sánh dựa trên thuộc tính Province của Location
                .Include(j => j.Recruiter)
                .Include(j => j.Category)
                .Include(j => j.Location) // Đảm bảo Location được bao gồm trong truy vấn
				.Include(j => j.Links)      // Load Links
				.Include(j => j.Skills) 
				.AsNoTracking()
                .OrderBy(j => j.Title) // Sắp xếp theo tiêu đề công việc
                .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(lstjobrequest); // Trả về view chứa danh sách công việc được tìm thấy
        }

    }
}
