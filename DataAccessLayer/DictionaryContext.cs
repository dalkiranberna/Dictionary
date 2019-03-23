using Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DictionaryContext : IdentityDbContext<Person>
    {
		public DictionaryContext() : base("DictionaryContext")
		{

		}
        
		public virtual DbSet<Language> Languages { get; set; }
		public virtual DbSet<Word> Words { get; set; }
		public virtual DbSet<WordRequest> WordRequests { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			#region TableConfiguration
			modelBuilder.Entity<Language>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<Word>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<WordRequest>()
				.HasKey(x => x.ID);
			#endregion

			#region Relations
			modelBuilder.Entity<Language>()
				.HasMany(x => x.Words)
				.WithRequired(x => x.Language);

			modelBuilder.Entity<Word>() //kendisiyle çoka çok ilişkisi var.
				.HasMany(x => x.Tranlations)
				.WithMany();

			modelBuilder.Entity<WordRequest>()
				.HasRequired(x => x.Person)
				.WithMany(x => x.Requests);

			modelBuilder.Entity<WordRequest>()
				.HasRequired(x => x.Language)
				.WithMany(x => x.Requests)
				.HasForeignKey(x => x.LanguageId);
			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}
