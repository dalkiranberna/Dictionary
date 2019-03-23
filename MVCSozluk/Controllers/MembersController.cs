using BusinessLogicLayer;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSozluk.Controllers
{
    public class MembersController : Controller
    {
		UnitOfWork _uw = new UnitOfWork();
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult LogOff()
		{
			HttpContext.GetOwinContext().Authentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult ChangePass()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ChangePass(string oldp, string newp)
		{
			UserStore<Person> store = new UserStore<Person>(_uw.db); 
			UserManager<Person> manager = new UserManager<Person>(store);

			string uId = User.Identity.GetUserId();
			Person person = _uw.db.Users.Find(uId);
			bool isCorrect = manager.CheckPassword(person, oldp);

			if (isCorrect)
			{
				IdentityResult r = manager.ChangePassword(uId, oldp, newp);
				if (r.Succeeded)
					ViewBag.Success = true;
				else
					ViewBag.Errors = r.Errors;
			}
			else
				ViewBag.WrongPassword = true;

			return View();
		}

		public ActionResult MyAccount()
		{
			string uId = User.Identity.GetUserId();
			Person person = _uw.db.Users.Find(uId);
			MyAccountViewModel vm = new MyAccountViewModel();
			vm.Email = person.Email;
			vm.PhoneNumber = person.PhoneNumber;
			if (person.HasPhoto)
				ViewBag.Photo = "/Uploads/Members/" + uId + ".jpg";
			return View(vm);
		}

		[HttpPost]
		public ActionResult MyAccount(MyAccountViewModel info, HttpPostedFileBase imgFile)
		{
			UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());
			UserManager<Person> manager = new UserManager<Person>(store);
			string uId = User.Identity.GetUserId();
			Person person = manager.FindById(uId);

			person.Email = info.Email;
			person.PhoneNumber = info.PhoneNumber;

			if (imgFile != null)
			{
				string path = Server.MapPath("/Uploads/Members/");
				string old = path + person.Id + ".jpg";
				if (System.IO.File.Exists(old))
					System.IO.File.Delete(old);

				string _new = path + person.Id + ".jpg";
				imgFile.SaveAs(_new);

				person.HasPhoto = true;
			}

			manager.Update(person);
			
			if (person.HasPhoto)
				ViewBag.Photo = "/Uploads/Members/" + uId + ".jpg"; 

			return View(info);
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(Person person, string Pass, HttpPostedFileBase img)
		{
			UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());
			UserManager<Person> manager = new UserManager<Person>(store);
			var result = manager.Create(person, Pass); 

			if (result.Succeeded) //kayıt başarılıysa
			{

			if (img != null)
				{
					string path = Server.MapPath("/Uploads/Members/");
					img.SaveAs(path + person.Id + ".jpg");
					person.HasPhoto = true;

					manager.Update(person); 
				}

				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Errors = result.Errors;
			}

			return View();
		}

		public ActionResult _LoginModal() 
		{
			return View();
		}

		public JsonResult Login(LoginViewModel info)
		{
			ApplicationSignInManager signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            
			SignInStatus result = signInManager.PasswordSignIn(info.Email, info.Password, true, false);
            
			switch (result)
			{
				case SignInStatus.Success:
					return Json(new { success=true});
				case SignInStatus.Failure:
					return Json(new { success = false });
			}
			return Json(new { success = false });
		}
    }
}