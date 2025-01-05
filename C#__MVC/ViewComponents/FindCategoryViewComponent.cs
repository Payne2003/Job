using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace C___MVC.ViewComponents
{
	public class FindCategoryViewComponent : ViewComponent
	{
		JobContext JobContext;
		List<Category> categories;
		public FindCategoryViewComponent(JobContext jobContext)
		{
			JobContext = jobContext;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var category = JobContext.Categories.ToList();
			return View("RenderFindCategoryView", category);
		}
	}
}
