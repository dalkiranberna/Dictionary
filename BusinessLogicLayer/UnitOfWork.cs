using DataAccessLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
	public class UnitOfWork
	{
		public DictionaryContext db;
		public UnitOfWork()
		{
			object whatever = ""; 
			if (db == null)
			{
				lock (whatever) 
				{
					if (db == null)
						db = new DictionaryContext();
				}
				
			}

			Languages = new BaseRepository<Language>(db);
			Words = new BaseRepository<Word>(db);
			WordRequests = new BaseRepository<WordRequest>(db);
		}
		public static DictionaryContext Create() 
		{
			return new DictionaryContext();
		}

		public bool Complete() 
		{
			try
			{
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public BaseRepository<Language> Languages;
		public BaseRepository<Word> Words;
		public BaseRepository<WordRequest> WordRequests;
	}
}
