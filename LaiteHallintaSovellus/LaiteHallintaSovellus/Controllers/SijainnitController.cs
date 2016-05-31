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
    public class SijainnitController : Controller
    {
        private Laiterekisteri_dbEntities db = new Laiterekisteri_dbEntities();

        // GET: Sijainnit
        public ActionResult Index()
        {
            var sijainnit = db.Sijainnit.Include(s => s.Laitteet).Include(s => s.Varastot);
            return View(sijainnit.ToList());
        }

        // GET: Sijainnit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sijainnit sijainnit = db.Sijainnit.Find(id);
            if (sijainnit == null)
            {
                return HttpNotFound();
            }
            return View(sijainnit);
        }

        // GET: Sijainnit/Create
        public ActionResult Create()
        {
            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi");
            ViewBag.Varasto_ID = new SelectList(db.Varastot, "Varasto_ID", "Varasto");
            return View();
        }

        // POST: Sijainnit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sijainti_ID,Varasto_ID,Laite_ID")] Sijainnit sijainnit)
        {
            if (ModelState.IsValid)
            {
                // Lisätään tauluun päivämäärä tieto
                sijainnit.Pvm = DateTime.Today;
                db.Sijainnit.Add(sijainnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            DateTime paiva = DateTime.Today;
            ViewBag.pvm = paiva; 
            /*paiva = DateTime.Today;
            ViewBag.paiva;*/
            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi", sijainnit.Laite_ID);
            ViewBag.Varasto_ID = new SelectList(db.Varastot, "Varasto_ID", "Varasto", sijainnit.Varasto_ID);
            return View(sijainnit);
        }

        // GET: Sijainnit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sijainnit sijainnit = db.Sijainnit.Find(id);
            if (sijainnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi", sijainnit.Laite_ID);
            ViewBag.Varasto_ID = new SelectList(db.Varastot, "Varasto_ID", "Varasto", sijainnit.Varasto_ID);
            return View(sijainnit);
        }

        // POST: Sijainnit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sijainti_ID,Varasto_ID,Laite_ID,Pvm")] Sijainnit sijainnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sijainnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi", sijainnit.Laite_ID);
            ViewBag.Varasto_ID = new SelectList(db.Varastot, "Varasto_ID", "Varasto", sijainnit.Varasto_ID);
            return View(sijainnit);
        }

        // GET: Sijainnit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sijainnit sijainnit = db.Sijainnit.Find(id);
            if (sijainnit == null)
            {
                return HttpNotFound();
            }
            return View(sijainnit);
        }

        // POST: Sijainnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sijainnit sijainnit = db.Sijainnit.Find(id);
            db.Sijainnit.Remove(sijainnit);
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
