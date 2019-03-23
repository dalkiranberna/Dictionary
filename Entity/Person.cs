using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Person : IdentityUser
	{
		public DateTime RegisterDate { get; set; }
		public bool HasPhoto { get; set; }
		public virtual List<WordRequest> Requests { get; set; }

		public Person()
		{
			RegisterDate = DateTime.Now;
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Person> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}
