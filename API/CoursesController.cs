using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using WU15.StudentAdministration.Web.Models;

namespace WU15.StudentAdministration.Web.API
{
    public class CoursesController : ApiController
    {
        [HttpGet]
        public IEnumerable<Course> Get(string sid)
        {            
            return MvcApplication.Courses.Where(x => x.SchoolNo.Equals(sid));
        }

        public Course Get(int id)
        {
            return MvcApplication.Courses.FirstOrDefault(x => x.Id == id);
        }

        public string Post(Course course)
        {
            if (course.Id == 0)
            {
                if (MvcApplication.Courses.Any())
                {
                    var id = MvcApplication.Courses.Max(x => x.Id) + 1;
                    course.Id = id;
                }
                else
                {
                    course.Id = 1;
                }
            }
            else
            {
                var savedIndex = MvcApplication.Courses.FindIndex(x => x.Id == course.Id);
                MvcApplication.Courses.RemoveAt(savedIndex);                
            }
            MvcApplication.Courses.Add(course);

            return course.Name;
        }

        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            var course = MvcApplication.Courses.FirstOrDefault(x => x.Id == id);
            MvcApplication.Courses.Remove(course);
        }
    }
}
