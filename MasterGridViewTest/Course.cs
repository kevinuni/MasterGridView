using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGridViewTest
{
    public class Course
    {
        public string Subject{ get; set; }

        public string Teacher { get; set; }

        public int Credits { get; set; }

        public Course() { }

        public Course(string subject, string teacher)
        {
            Subject = subject;
            Teacher = teacher;            
        }

        public static List<Course> getCourses()
        {
            List<Course> list = new List<Course>();
            list.Add(new Course() { Subject = "Principal", Teacher = "Gene Vagina", Credits = 10});
            list.Add(new Course() { Subject = "Math", Teacher = "Mr. Goldenfold", Credits = 10 });

            return list;
        }
    }
}
