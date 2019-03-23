using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Language : IEntity
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public virtual List<Word> Words { get; set; }
		public virtual List<WordRequest> Requests { get; set; }
	}
}
