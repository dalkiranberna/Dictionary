using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class WordRequest : IEntity
	{
		public int ID { get; set; }
		public string Word { get; set; }
		public int LanguageId { get; set; }
		public virtual Language Language { get; set; }
		public virtual Person Person { get; set; }

	}
}
