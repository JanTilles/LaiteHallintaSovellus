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
    public class LaitteetController : Controller
    {
        private Laiterekisteri_dbEntities db = new Laiterekisteri_dbEntities();

        // GET: Laitteet
        public ActionResult Index(string hakuSana)
        {
            var laitteet = from l in db.Laitteet
                         select l;

            if (!String.IsNullOrEmpty(hakuSana))
            {
                laitteet = laitteet.Where(s => s.Tyyppi.Contains(hakuSana) || s.Sarjanumero.Contains(hakuSana) || s.Valmistaja.Contains(hakuSana) || s.Malli.Contains(hakuSana) || s.QR_Koodi.Contains(hakuSana));
            }
            return View(laitteet);
            //return View(db.Laitteet.ToList());
        }

        // GET: Laitteet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laitteet laitteet = db.Laitteet.Find(id);
            if (laitteet == null)
            {
                return HttpNotFound();
            }
            return View(laitteet);
        }

        // GET: Laitteet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laitteet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Laite_ID,Tyyppi,Malli,Valmistaja,Sarjanumero,Hankinta_Pvm,Takuu_Aika,QR_Koodi")] Laitteet laitteet)
        {
            if (ModelState.IsValid)
            {
                db.Laitteet.Add(laitteet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laitteet);
        }

        // GET: Laitteet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laitteet laitteet = db.Laitteet.Find(id);
            if (laitteet == null)
            {
                return HttpNotFound();
            }
            return View(laitteet);
        }

        // POST: Laitteet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Laite_ID,Tyyppi,Malli,Valmistaja,Sarjanumero,Hankinta_Pvm,Takuu_Aika,QR_Koodi")] Laitteet laitteet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laitteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laitteet);
        }

        // GET: Laitteet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laitteet laitteet = db.Laitteet.Find(id);
            if (laitteet == null)
            {
                return HttpNotFound();
            }
            return View(laitteet);
        }

        // POST: Laitteet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laitteet laitteet = db.Laitteet.Find(id);
            db.Laitteet.Remove(laitteet);
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
