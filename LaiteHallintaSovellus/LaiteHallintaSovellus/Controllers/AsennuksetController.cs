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
    public class AsennuksetController : Controller
    {
        private Laiterekisteri_dbEntities db = new Laiterekisteri_dbEntities();

        // GET: Asennukset
        public ActionResult Index()
        {
            var asennukset = db.Asennukset.Include(a => a.Laitteet).Include(a => a.Ohjelmat);
            return View(asennukset.ToList());
        }

        // GET: Asennukset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asennukset asennukset = db.Asennukset.Find(id);
            if (asennukset == null)
            {
                return HttpNotFound();
            }
            return View(asennukset);
        }

        // GET: Asennukset/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Laite_ID=id;
            //ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi");
            ViewBag.Ohjelma_ID = new SelectList(db.Ohjelmat, "Ohjelma_ID", "Ohjelma");
            return View();

        }

        // POST: Asennukset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsennusID,Ohjelma_ID,Laite_ID")] Asennukset asennukset)
        {
            if (ModelState.IsValid)
            {
                db.Asennukset.Add(asennukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi", asennukset.Laite_ID);
            ViewBag.Ohjelma_ID = new SelectList(db.Ohjelmat, "Ohjelma_ID", "Ohjelma", asennukset.Ohjelma_ID);
            return View(asennukset);
        }

        // GET: Asennukset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asennukset asennukset = db.Asennukset.Find(id);
            if (asennukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi", asennukset.Laite_ID);
            ViewBag.Ohjelma_ID = new SelectList(db.Ohjelmat, "Ohjelma_ID", "Ohjelma", asennukset.Ohjelma_ID);
            return View(asennukset);
        }

        // POST: Asennukset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsennusID,Ohjelma_ID,Laite_ID")] Asennukset asennukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asennukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Laite_ID = new SelectList(db.Laitteet, "Laite_ID", "Tyyppi", asennukset.Laite_ID);
            ViewBag.Ohjelma_ID = new SelectList(db.Ohjelmat, "Ohjelma_ID", "Ohjelma", asennukset.Ohjelma_ID);
            return View(asennukset);
        }

        // GET: Asennukset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asennukset asennukset = db.Asennukset.Find(id);
            if (asennukset == null)
            {
                return HttpNotFound();
            }
            return View(asennukset);
        }

        // POST: Asennukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asennukset asennukset = db.Asennukset.Find(id);
            db.Asennukset.Remove(asennukset);
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
