using C___MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions; // Thêm namespace này để sử dụng phân trang
namespace C___MVC.Controllers
{
	public class CandidatesController : Controller
    {
        private readonly JobContext _context;
        public CandidatesController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 3; // Số lượng bản ghi trên mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại, mặc định là 1

            var candidates = _context.Candidates
                                     .Include(c => c.Resumes)
                                     .ThenInclude(r => r.Job)
                                     .Select(c => new
                                     {
                                         c.CandidateId,
                                         c.UserName,
                                         c.Email,
                                         c.PhoneNumber,
                                         c.Company,
                                         c.Avatar,
                                         c.Cv,
                                         YearsSinceCreation = (DateTime.Now.Year - c.CreatedAt.Year),
                                         JobTitle = c.Resumes.Select(r => r.Job.Title).FirstOrDefault() ?? "No jobs applied yet"
                                     })
                                     .ToPagedList(pageNumber, pageSize);

            // Trả về PartialView nếu là yêu cầu Ajax
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_CandidateList", candidates);
            }

            // Nếu không phải yêu cầu Ajax, trả về View đầy đủ
            return View(candidates);
        }




        [HttpPost]
		public IActionResult Search(string? query, int? page)
		{
			var require = _context.Jobs.AsQueryable();

			if (!string.IsNullOrEmpty(query))
			{
				require = require.Where(j => j.Title.Contains(query));
			}

			var candidates = _context.Candidates
									 .Include(c => c.Resumes)
									 .ThenInclude(r => r.Job)
									 .Where(c => c.Resumes.Any(r => require.Contains(r.Job)))
									 .Select(c => new
									 {
										 c.UserName,
										 c.Email,
										 c.PhoneNumber,
										 c.Company,
										 c.Avatar,
										 YearsSinceCreation = (DateTime.Now.Year - c.CreatedAt.Year
															   - ((DateTime.Now.DayOfYear < c.CreatedAt.DayOfYear) ? 1 : 0)),
										 JobTitle = c.Resumes.Select(r => r.Job.Title).FirstOrDefault()
									 })
									 .ToPagedList(page ?? 1, 3); // Phân trang với 3 ứng viên mỗi trang

			return PartialView("Search", candidates);
		}
	}
}
