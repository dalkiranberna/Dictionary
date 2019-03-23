using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSozluk.Controllers
{
	public class HomeController : Controller
	{
		UnitOfWork _uw = new UnitOfWork();
		public ActionResult Index()
		{
			ViewBag.Langs = _uw.Languages.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.ID.ToString()
			});
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}