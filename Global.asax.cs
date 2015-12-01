using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;
using WU15.StudentAdministration.Web.API;
using WU15.StudentAdministration.Web.Handlers;
using WU15.StudentAdministration.Web.Models;

namespace WU15.StudentAdministration.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static List<Course> Courses = null;
        public static List<Student> Students = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new XHttpMethodOverrideDelegatingHandler());

            Courses = new List<Course>();
            Students = new List<Student>();
            LoadStudents();
            LoadCourses();

            KeepAliveThread.Start();
        }

        private static void LoadCourses()
        {
            var course = new Course()
            {
                Id = 1,
                Students = Students,
                SchoolNo = "c08bdab7-ed3d-4048-8338-d4f14f2770a8",
                Credits = "15",
                Name = "Pedagogik 1",
                Term = "VT",
                Year = "2015",
                Active = false
            };
            Courses.Add(course);

            course = new Course()
            {
                Id = 2,
                //Students = Students,
                SchoolNo = "c08bdab7-ed3d-4048-8338-d4f14f2770a8",
                Credits = "10",
                Name = "Pedagogik 2",
                Term = "VT",
                Year = "2015",
                Active = false
            };
            Courses.Add(course);

            course = new Course()
            {
                Id = 3,
                //Students = Students,
                SchoolNo = "c08bdab7-ed3d-4048-8338-d4f14f2770a8",
                Credits = "5",
                Name = "Datalogi 1",
                Term = "VT",
                Year = "2015",
                Active = true
            };
            Courses.Add(course);

            course = new Course()
            {
                Id = 4,
                //Students = Students,
                SchoolNo = "c08bdab7-ed3d-4048-8338-d4f14f2770a8",
                Credits = "7,5",
                Name = "Filosofi 1",
                Term = "VT",
                Year = "2015",
                Active = true
            };
            Courses.Add(course);
        }

        private static IEnumerable<Student> GetRandomStudentList()
        {
            var list = new List<Student>();
            var random = new Random();

            var index = random.Next(0, 4);
            for (var i = 0; i < index; i++)
            {
                var subIndex = random.Next(0, 4);
                list.Add(Students.ElementAt(subIndex));

            }

            return Students;
        }

        private static void LoadStudents()
        {
            var student = new Student
            {
                Id = 1,
                FirstName = "Kalle",
                LastName = "Bengtsson",
                Personnummer = "760101-1111",
                Active = true
            };
            Students.Add(student);

            student = new Student
            {
                Id = 2,
                FirstName = "Eva",
                LastName = "Andersson",
                Personnummer = "760101-1111",
                Active = true
            };
            Students.Add(student);

            student = new Student
            {
                Id = 3,
                FirstName = "Ylva",
                LastName = "Nordsson",
                Personnummer = "760101-1111",
                Active = true
            };
            Students.Add(student);

            student = new Student
            {
                Id = 4,
                FirstName = "Evy",
                LastName = "Carlsson",
                Personnummer = "760101-1111",
                Active = true
            };
            Students.Add(student);

            student = new Student
            {
                Id = 5,
                FirstName = "Lisa",
                LastName = "Olofsson",
                Personnummer = "760101-1111",
                Active = true
            };
            Students.Add(student);

            student = new Student
            {
                Id = 6,
                FirstName = "Robert",
                LastName = "Tovek",
                Personnummer = "760101-1111",
                Active = true
            };
            Students.Add(student);
        }

        static readonly Thread KeepAliveThread = new Thread(KeepAlive);

        protected void Application_End()
        {
            KeepAliveThread.Abort();
        }

        static void KeepAlive()
        {
            while (true)
            {
                WebRequest req = WebRequest.Create("http://api.wu15.se/keepalive/index");

                try
                {
                    req.GetResponse();

                    try
                    {
                        Thread.Sleep(30000);
                    }
                    catch (ThreadAbortException)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                }



            }
        }

        void Application_End(object sender, EventArgs e)
        {
        }
    }


}
