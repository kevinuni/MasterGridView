using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Qualifications
    {
        [DescriptionAttribute("Id")]
        public int Id { get; set; }

        [DescriptionAttribute("Primera nota")]
        public int Nota1 { get; set; }

        [DescriptionAttribute("Segunda nota")]
        public int Nota2 { get; set; }

        public Qualifications(int id, int nota1, int nota2) 
        {
            Id = id;
            Nota1 = nota1;
            Nota2 = nota2;
        }

        public Qualifications() { }
        public static List<Qualifications> getNotes() 
        {
            List<Qualifications> list = new List<Qualifications>();
            list.Add(new Qualifications(1, 10, 15));
            list.Add(new Qualifications(2, 12, 20));
            list.Add(new Qualifications(3, 5, 23));

            return list;
        }
    }
}
