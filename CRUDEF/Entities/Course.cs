using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDEF.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }

        public Course()
        {
            StudentCourses = new List<StudentCourse>();
        }
    }
}
