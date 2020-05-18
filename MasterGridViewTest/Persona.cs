using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Person
    {
        [DescriptionAttribute("Id")]
        public int Id { get; set; }

        [DescriptionAttribute("Student First Name")]
        public string FirstName { get; set; }

        [DescriptionAttribute("Student Last Name")]
        public string LastName { get; set; }

        [DescriptionAttribute("Califications")]
        public List<Notes> lstNotes = new List<Notes>();

        public Person() 
        {

        
        }

        public Person(int id, string firstName, string lastName, List<Notes> notes) 
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            lstNotes = notes;
        }

        public static List<Person> getPeople()
        {
            List<Person> list = new List<Person>();
            list.Add(new Person(1, "Morty", "Smith", Notes.getNotes()));
            list.Add(new Person(2, "John", "Doe", Notes.getNotes()));
            list.Add(new Person(3, "Rick", "Sanchez", Notes.getNotes()));

            return list;
        }

       

    }
}
