using BusinessLogicLayer;
using Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSozluk.Controllers
{
    public class WordRequestController : Controller
    {
		UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index(int? del)
        {
			if (del.HasValue)
			{
				_uw.WordRequests.Delete(del.Value);
				_uw.Complete();
			}
			var list = _uw.WordRequests.GetAll();
            return View(list);
        }

		[HttpGet]
		public ActionResult Create()
		{
			ViewBag.Langs = _uw.Languages.GetAll()
				.Select(x => new SelectListItem 
				{
					Text = x.Name,
					Value = (x.ID).ToString()
				});
			return View();
		}

		[HttpPost]
		public ActionResult Create(WordRequest r)
		{
			if (ModelState.IsValid)
			{
				string uId = User.Identity.GetUserId();
				var user = _uw.db.Users.Find(uId);
				r.Person = user;
				_uw.WordRequests.Add(r);
				_uw.Complete(); 
				return RedirectToAction("Index");
			}

			ViewBag.Langs = _uw.Languages.GetAll()
				   .Select(x => new SelectListItem 
				   {
					   Text = x.Name,
					   Value = (x.ID).ToString()
				   });

			return View(r);
		}
	}
}