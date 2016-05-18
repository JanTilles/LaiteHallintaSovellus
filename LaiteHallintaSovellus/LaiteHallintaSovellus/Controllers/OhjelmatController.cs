using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaiteHallintaSovellus.Models;

namespace LaiteHallintaSovellus.Controllers
{
    public class OhjelmatController : Controller
    {
        private Laiterekisteri_dbEntities db = new Laiterekisteri_dbEntities();

        // GET: Ohjelmat
        public ActionResult Index()
        {
            return View(db.Ohjelmat.ToList());
        }

        // GET: Ohjelmat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ohjelmat ohjelmat = db.Ohjelmat.Find(id);
            if (ohjelmat == null)
            {
                return HttpNotFound();
            }
            return View(ohjelmat);
        }

        // GET: Ohjelmat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ohjelmat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ohjelma_ID,Ohjelma")] Ohjelmat ohjelmat)
        {
            if (ModelState.IsValid)
            {
                db.Ohjelmat.Add(ohjelmat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ohjelmat);
        }

        // GET: Ohjelmat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ohjelmat ohjelmat = db.Ohjelmat.Find(id);
            if (ohjelmat == null)
            {
                return HttpNotFound();
            }
            return View(ohjelmat);
        }

        // POST: Ohjelmat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ohjelma_ID,Ohjelma")] Ohjelmat ohjelmat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ohjelmat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ohjelmat);
        }

        // GET: Ohjelmat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ohjelmat ohjelmat = db.Ohjelmat.Find(id);
            if (ohjelmat == null)
            {
                return HttpNotFound();
            }
            return View(ohjelmat);
        }

        // POST: Ohjelmat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ohjelmat ohjelmat = db.Ohjelmat.Find(id);
            db.Ohjelmat.Remove(ohjelmat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
