using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Notes
    {
        [DescriptionAttribute("Id")]
        public int Id { get; set; }

        [DescriptionAttribute("Primera nota")]
        public int Nota1 { get; set; }

        [DescriptionAttribute("Segunda nota")]
        public int Nota2 { get; set; }

        public Notes(int id, int nota1, int nota2) 
        {
            Id = id;
            Nota1 = nota1;
            Nota2 = nota2;
        }

        public Notes() { }
        public static List<Notes> getNotes() 
        {
            List<Notes> list = new List<Notes>();
            list.Add(new Notes(1, 10, 15));
            list.Add(new Notes(2, 12, 20));
            list.Add(new Notes(3, 5, 23));

            return list;
        }
    }
}
