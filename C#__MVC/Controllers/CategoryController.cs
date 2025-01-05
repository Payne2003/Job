using Azure;
using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace C___MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly JobContext _context;
        public CategoryController(JobContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstCategory = _context.Categories.AsNoTracking().OrderBy(x=>x.CategoryId);
            PagedList<Category> lst = new PagedList<Category>(lstCategory, pageNumber, pageSize);
            return View(lst);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    TempData["error"] = "Category name cannot be empty."; // Thông báo lỗi
                    return View(category); // Trả về view với dữ liệu lỗi
                }
                _context.Categories.Add(category);
                _context.SaveChanges();

                TempData["success"] = "Category created successfully!"; // Thêm thông báo thành công
                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to create category."; // Thêm thông báo lỗi
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            var category = _context.Categories.Find(id);
            
            if (category == null)
            {
                TempData["error"] = "Category not found."; // Thêm thông báo lỗi nếu không tìm thấy
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    TempData["error"] = "Category name cannot be empty."; // Thông báo lỗi
                    return View(category); // Trả về view với dữ liệu lỗi
                }
                _context.Categories.Update(category);
                _context.SaveChanges();

                TempData["success"] = "Category updated successfully!"; // Thêm thông báo thành công
                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to update category."; // Thêm thông báo lỗi
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                TempData["error"] = "Category not found."; // Thêm thông báo lỗi nếu không tìm thấy
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["success"] = "Category deleted successfully!"; // Thêm thông báo thành công
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryDetails([FromBody] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Category ID.");
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Json(category); // Đảm bảo trả về dữ liệu JSON
        }
        [HttpGet]
        public IActionResult Search(string? query, int? page)
        {
            int pageSize = 3; // Số lượng danh mục mỗi trang
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            // Xử lý query null hoặc chuỗi trắng
            query = query?.Trim() ?? "";

            // Truy vấn với điều kiện lọc và phân trang
            var categories = _context.Categories
                .Where(c => string.IsNullOrEmpty(query) || c.Name.Contains(query))
                .AsNoTracking()
                .OrderBy(c => c.Name) // Sắp xếp theo tên danh mục
                .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(categories); // Trả về view với danh sách đã phân trang
        }


    }
}
