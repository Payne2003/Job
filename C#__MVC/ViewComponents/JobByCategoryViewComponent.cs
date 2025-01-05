using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace C___MVC.ViewComponents
{
	public class JobByCategoryViewComponent : ViewComponent
	{
		private readonly JobContext _jobContext;
		List<Category> categories;
		public JobByCategoryViewComponent(JobContext jobContext)
		{
			_jobContext = jobContext;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _jobContext.Categories.ToListAsync();

			// Truyền danh sách categories đến view RenderCategory
			return View("RenderJobByCategory", categories);
		}
	}
}
