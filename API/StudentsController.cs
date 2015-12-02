using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WU15.StudentAdministration.Web.DataAccess;
using WU15.StudentAdministration.Web.Models;
using System.Data.Entity;


namespace WU15.StudentAdministration.Web.API
{
    public class StudentsController : ApiController
    {
        private DefaultDataContext db = new DefaultDataContext();
        public IEnumerable<Student> Get()
        {
            var students = db.Students.Include("Courses").OrderBy(x => x.FirstName);

            return students;
        }

        public Student Get(int id)
        {
            return db.Students.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public string Post(Student student)
        {
            if (student.Id > 0) // save
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            }
            else // add
            {
                db.Students.Add(student);
                //var savedIndex = MvcApplication.Students.FindIndex(x => x.Id == student.Id);
                //MvcApplication.Students.RemoveAt(savedIndex);
            }
            db.SaveChanges();

            return string.Format("{0} {1}", student.FirstName, student.LastName);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var student = MvcApplication.Students.FirstOrDefault(x => x.Id == id);
            MvcApplication.Students.Remove(student);
        }
    }
}
