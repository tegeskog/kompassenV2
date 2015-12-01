using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WU15.StudentAdministration.Web.Controllers
{
    public class KeepAliveController : Controller
    {
        // GET: KeepAlive
        public ActionResult Index()
        {
            return View();
        }

        // GET: KeepAlive/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KeepAlive/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KeepAlive/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KeepAlive/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KeepAlive/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KeepAlive/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KeepAlive/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
