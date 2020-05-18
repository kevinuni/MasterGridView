using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Student
    {
        [DescriptionAttribute("Id")]
        public int Id { get; set; }

        [DescriptionAttribute("Student First Name")]
        public string FirstName { get; set; }

        [DescriptionAttribute("Student Last Name")]
        public string LastName { get; set; }

        [DescriptionAttribute("Qualifications")]
        public List<Qualifications> lstQualifications = new List<Qualifications>();

        [DescriptionAttribute("Course Names")]
        public List<Course> lstCourses = new List<Course>();

        public Student() 
        {

        
        }

        public Student(int id, string firstName, string lastName, List<Qualifications> notes) 
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            lstQualifications = notes;
        }

        public static List<Student> getStudents()
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { Id = 1 , FirstName = "Morty", LastName = "Smith", lstQualifications = Qualifications.getNotes(), lstCourses = Course.getCourses()});
            list.Add(new Student() { Id = 1, FirstName = "Rick", LastName = "Sanchez", lstQualifications = Qualifications.getNotes(), lstCourses = Course.getCourses() });
            list.Add(new Student() { Id = 1, FirstName = "John", LastName = "Doe", lstQualifications = Qualifications.getNotes(), lstCourses = Course.getCourses() });

            
            return list;
        }

       

    }
}
