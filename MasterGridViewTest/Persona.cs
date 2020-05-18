using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Persona
    {
        [DescriptionAttribute("Id")]
        public int Id { get; set; }

        [DescriptionAttribute("Nombre de la persona")]
        public string Nombre { get; set; }

        [DescriptionAttribute("Apellido de la persona")]
        public string Apellido { get; set; }

        [DescriptionAttribute("Notas de la persona")]
        public List<Nota> lstNotas = new List<Nota>();

        public Persona() 
        {

        
        }

        public Persona(int id, string nombre, string apellido, List<Nota> notas) 
        {
            Nombre = nombre;
            Apellido = apellido;
            Id = id;
            lstNotas = notas;
        }

        public static List<Persona> getPersonas()
        {
            List<Persona> list = new List<Persona>();
            list.Add(new Persona(1, "Periodo", "Palotes", Nota.getNotas()));
            list.Add(new Persona(2, "John", "Doe", Nota.getNotas()));
            list.Add(new Persona(3, "Juan", "Perez", Nota.getNotas()));

            return list;
        }

        public static ArrayList getArrayListPersonas()
        {
            ArrayList list = new ArrayList();
            list.Add(new Persona(1, "Periodo", "Palotes", Nota.getNotas()));
            list.Add(new Persona(2, "John", "Doe", Nota.getNotas()));
            list.Add(new Persona(3, "Juan", "Perez", Nota.getNotas()));
            return list;
        }

    }
}
