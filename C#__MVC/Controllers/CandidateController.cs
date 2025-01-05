using Azure;
using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using X.PagedList;
using X.PagedList.Extensions;

namespace C___MVC.Controllers
{
    public class CandidateController : Controller
    {
        private readonly JobContext _context;
        public CandidateController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstCandidate = _context.Candidates.Include(l => l.Links)
                .Include(l => l.Skills).AsNoTracking().OrderBy(x=>x.CandidateId);
            PagedList<Candidate> lst = new PagedList<Candidate>(lstCandidate, pageNumber, pageSize);
            return View(lst);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra xem Username và Email có trống không
                if (string.IsNullOrWhiteSpace(candidate.UserName) || string.IsNullOrWhiteSpace(candidate.Email))
                {
                    TempData["error"] = "Failed to add candidate: Username and Email cannot be empty."; // Thông báo lỗi
                    return View(candidate); // Trả về view với dữ liệu lỗi
                }
                // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu chưa
                var existingCandidate = await _context.Candidates
                    .FirstOrDefaultAsync(c => c.Email == candidate.Email);

                if (existingCandidate != null)
                {
                    TempData["error"] = "Failed to add candidate: Email already exists."; // Thông báo lỗi khi email đã tồn tại
                    return View(candidate); // Trả về view với candidate để hiển thị lỗi
                }

                candidate.EmailConfirmed = true; // Đặt trạng thái xác nhận email là true
                candidate.Avatar = "avatar_user.png"; // Gán giá trị mặc định nếu không có ảnh
                candidate.Password = HashPassword(candidate.Password); // Mã hóa mật khẩu

                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();

                TempData["success"] = "Candidate added successfully!"; // Thêm thông báo thành công
                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to add candidate."; // Thêm thông báo lỗi nếu không thành công
            return View(candidate); // Trả về view với candidate để hiển thị lỗi
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Tìm Candidate theo id
            var candidate = _context.Candidates
                .Include(l => l.Links)
                .Include(s => s.Skills)
                .Include(r => r.Resumes)
                .FirstOrDefault(c => c.CandidateId == id); // Sử dụng lambda expression để tìm candidate

            if (candidate == null)
            {
                TempData["error"] = "Candidate not found.";
                return Json(new { success = false });
            }

            // Xóa ảnh liên quan nếu có
            if (!string.IsNullOrEmpty(candidate.Avatar))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", candidate.Avatar);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Xóa candidate
            _context.Candidates.Remove(candidate);
            _context.SaveChanges();

            TempData["success"] = "Candidate deleted successfully!";
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> GetCandidateDetails([FromBody] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid candidate ID.");
            }

            // Tìm kiếm Candidate bao gồm cả Links và Skills
            var candidate = await _context.Candidates
                .Include(c => c.Links)     // Bao gồm dữ liệu Links
                .Include(c => c.Skills)    // Bao gồm dữ liệu Skills
                .Where(c => c.CandidateId == id)
                .Select(c => new
                {
                    c.CandidateId,
                    c.UserName,
                    c.Email,
                    c.PhoneNumber,
                    c.Avatar,
                    c.Gender,
                    c.countryside,
                    c.Currentjob,
                    c.CreatedAt,
                    c.UpdatedAt,
                    Links = c.Links.Select(l => new
                    {
                        l.Name,
                        l.Description
                    }),
                    Skills = c.Skills.Select(s => new
                    {
                        s.Name,
                        s.Description
                    })
                })
                .FirstOrDefaultAsync();

            if (candidate == null)
            {
                return NotFound("Candidate not found.");
            }

            return Json(candidate); // Trả về JSON chứa tất cả thông tin
        }


        [HttpGet]
        public IActionResult Search(string? query, int? page)
        {
            int pageSize = 3; // Số lượng ứng viên mỗi trang
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Xử lý query null hoặc chuỗi trắng
            query = query?.Trim() ?? "";

            // Truy vấn với điều kiện lọc và phân trang
            var candidates = _context.Candidates
                .Include(l => l.Links)
                .Include(l => l.Skills)
                .Where(c => string.IsNullOrEmpty(query)
                            || c.UserName.Contains(query)
                            || c.Email.Contains(query)
                            || c.PhoneNumber.Contains(query))
                .AsNoTracking()
                .OrderBy(c => c.UserName) // Sắp xếp theo UserName
                .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(candidates); // Trả về view với danh sách đã phân trang
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
