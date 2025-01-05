using Azure;
using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace C___MVC.Controllers
{
    public class JobRequestController : Controller
    {
        private readonly JobContext _context;
        public JobRequestController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstJobR = _context.Jobs
                              .Where(j => j.Status == false) // Lọc theo Status = 0 (false)
                              .Include(j => j.Recruiter)
                              .Include(j => j.Category)
                              .Include(j => j.Location)
                              .Include(j => j.Links)
                              .Include(j => j.Skills)
                              .AsNoTracking()
                              .OrderBy(x => x.JobId);

            PagedList<Job> lst = new PagedList<Job>(lstJobR, pageNumber, pageSize);
            return View(lst);
        }
        [HttpPost]
        public IActionResult Accept(int id)
        {
            // Kiểm tra ModelState
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid model state."; // Thông báo nếu model không hợp lệ
                return RedirectToAction("Index");
            }

            // Tìm Job theo id
            var job = _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.Category)
                .Include(j => j.Location)
                .Include(j => j.Links)
                .Include(j => j.Skills)
                .FirstOrDefault(j => j.JobId == id);

            if (job == null)
            {
                TempData["error"] = "Job not found.";
                return Json(new { success = false });
            }

            // Cập nhật Status của Job
            if (!job.Status) // Chỉ cập nhật nếu Status là false
            {
                job.Status = true; // Đặt Status = true
                _context.Update(job);
                _context.SaveChanges();

                TempData["success"] = "Job has been accepted successfully!";
            }
            else
            {
                TempData["info"] = "Job has already been accepted."; // Thông báo nếu job đã được chấp nhận
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            // Kiểm tra ModelState
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid model state."; // Thông báo nếu model không hợp lệ
                return Json(new { success = true });
            }

            // Tìm Job theo id
            var jobRequest = _context.Jobs
                .Include(j => j.Links)
                .Include(j => j.Skills)
                .FirstOrDefault(j => j.JobId == id);

            if (jobRequest == null)
            {
                TempData["error"] = "Job not found."; // Thêm thông báo lỗi nếu không tìm thấy Job
                return Json(new { success = false });
            }
            // Xóa các liên kết (Links) liên quan đến Recruiter
            if (jobRequest.Links != null && jobRequest.Links.Any())
            {
                _context.Links.RemoveRange(jobRequest.Links);
            }

            // Xóa các kỹ năng (Skills) liên quan đến Recruiter
            if (jobRequest.Skills != null && jobRequest.Skills.Any())
            {
                _context.Skills.RemoveRange(jobRequest.Skills);
            }
            // Xóa các kỹ năng (Skills) liên quan đến Recruiter
            if (jobRequest.Resumes != null && jobRequest.Resumes.Any())
            {
                _context.Resumes.RemoveRange(jobRequest.Resumes);
            }
            // Xóa Job, Links và Skills sẽ được xóa tự động
            _context.Jobs.Remove(jobRequest);
            _context.SaveChanges();

            TempData["success"] = "Job rejected successfully!"; // Thêm thông báo thành công
            return Json(new { success = true });
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
                .Where(j => j.Status == false && // Chỉ lấy các Job có Status là false
                            (string.IsNullOrEmpty(query)
                            || j.Title.Contains(query)
                            || j.Company.Contains(query)
                            || j.Location.Province.Contains(query))) // So sánh dựa trên thuộc tính Province của Location
                .Include(j => j.Recruiter)
                .Include(j => j.Category)
                .Include(j => j.Location) // Đảm bảo Location được bao gồm trong truy vấn
				.Include(j => j.Links)
				.Include(j => j.Skills)
				.AsNoTracking()
                .OrderBy(j => j.Title) // Sắp xếp theo tiêu đề công việc
                .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(lstjobrequest); // Trả về view chứa danh sách công việc được tìm thấy
        }

    }
}
