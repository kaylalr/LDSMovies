//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace LDSMovies.Controllers
//{
//	public class HomeController1 : Controller
//	{
//		// GET: /<controller>/
//		public IActionResult Index()
//		{
//			return View();
//		}
//	}
//}


using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace HelloWorld.Controllers
{
	public class HelloWorldController : Controller
	{
		// 
		// GET: /Index/

		public IActionResult Index()
		{
			return View();
		}

		//
		// GET: /Movies/Welcome/ 
		// Requires using System.Text.Encodings.Web;
		public IActionResult Welcome(string name, int numTimes = 1)
		{
			ViewData["Message"] = "Hello " + name;
			ViewData["NumTimes"] = numTimes;

			return View();
		}
	}
}