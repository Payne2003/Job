using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C___MVC.Data;
using C___MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace C___MVC.ViewComponents
{
	public class LocationViewComponent : ViewComponent
	{
		private readonly JobContext _jobContext;

		public LocationViewComponent(JobContext jobContext)
		{
			_jobContext = jobContext;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var locations = _jobContext.Locations.ToList();
			return View("RenderLocation", locations);
		}
	}
}
