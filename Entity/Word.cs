using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Word : IEntity
	{
		public int ID { get; set; }
		public string WordTxt { get; set; }
		public virtual Language Language { get; set; }
		public virtual List<Word> Tranlations { get; set; }
	}
}
