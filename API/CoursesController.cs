﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using WU15.StudentAdministration.Web.DataAccess;
using WU15.StudentAdministration.Web.Models;
using System.Data.Entity;


namespace WU15.StudentAdministration.Web.API
{
    public class CoursesController : ApiController
    {
        private DefaultDataContext db = new DefaultDataContext();

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return db.Courses.Include(x => x.Students).OrderByDescending(x => x.Active).ThenBy(x => x.Name);  
           
        }

        public Course Get(int id)
        {
            return db.Courses.Include(x => x.Students).FirstOrDefault(x => x.Id == id);
           
        }

        public string Post(Course course)
        {
            Course courseToUpdate = null;

            if (course.Id > 0)
            {
                courseToUpdate = db.Courses.Include("Students").First(i => i.Id == course.Id);
                
            }
            else
            {
                courseToUpdate = new Course();
                
            }

            courseToUpdate.Active = course.Active;
            courseToUpdate.Credits = course.Credits;
            courseToUpdate.Name = course.Name;
            courseToUpdate.Term = course.Term;
            courseToUpdate.Year = course.Year;

            foreach (var student in db.Students)
            {
                if (!course.Students.Any(item => item.Id == student.Id))
                {
                    courseToUpdate.Students.Remove(student);
                }
                else
                {
                    courseToUpdate.Students.Add((student));
                }

            }

            if (course.Id > 0)
            {
                db.Entry(courseToUpdate).State = System.Data.Entity.EntityState.Modified;
            } 
            else
            {
                db.Courses.Add(courseToUpdate);
            }
            db.SaveChanges();

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
