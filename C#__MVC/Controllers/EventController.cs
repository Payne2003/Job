using Microsoft.AspNetCore.Mvc;

namespace C___MVC.Controllers
{
	public class EventController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
