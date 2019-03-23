using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccessLayer;
using Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCSozluk.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : Person
    {

    }

	//UnityOfWork tanımladığım için bu contexte ihtiyacım yok! :
    /*public class ApplicationDbContext : DictionaryContext
	{
		public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }*/
}