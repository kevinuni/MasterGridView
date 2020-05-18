using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Nota
    {
        [DescriptionAttribute("Id")]
        public int Id { get; set; }

        [DescriptionAttribute("Primera nota")]
        public int Nota1 { get; set; }

        [DescriptionAttribute("Segunda nota")]
        public int Nota2 { get; set; }

        public Nota(int id, int nota1, int nota2) 
        {
            Id = id;
            Nota1 = nota1;
            Nota2 = nota2;
        }

        public Nota() { }
        public static List<Nota> getNotas() 
        {
            List<Nota> list = new List<Nota>();
            list.Add(new Nota(1, 10, 15));
            list.Add(new Nota(2, 12, 20));
            list.Add(new Nota(3, 5, 23));

            return list;
        }
    }
}
