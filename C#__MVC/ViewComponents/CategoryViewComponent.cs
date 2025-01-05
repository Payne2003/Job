using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace C___MVC.ViewComponents
{
	public class CategoryViewComponent : ViewComponent
	{
		JobContext jobContext;
		List<Category> categories;
		public CategoryViewComponent(JobContext jobContext)
		{
			this.jobContext = jobContext;
			
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{// Lấy tất cả các danh mục từ cơ sở dữ liệu
			var categories = await jobContext.Categories.ToListAsync();

			// Truyền danh sách categories đến view RenderCategory
			return View("RenderCategory", categories);
		}

	}
}
