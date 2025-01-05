using Microsoft.AspNetCore.Mvc;

namespace C___MVC.Controllers
{
	public class ServicesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult CreateCV()
		{
			return View();
		}
	}
}
