using System.Collections.Generic;
using Newtonsoft.Json;

namespace WU15.StudentAdministration.Web.Models
{
    public class Course
    {
        //[JsonProperty("courseId")]
        public int Id { get; set; }

        //[JsonProperty("courseName")]
        public string Name { get; set; }

        public string Term { get; set; }

        public string Year { get; set; }

        public string SchoolNo { get; set; }

        public string Credits { get; set; }

        public List<Student> Students { get; set; }

        public bool Active {get; set;}

        public Course()
        {
            Students = new List<Student>();
        }
    }
}
