using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ada_mvc;

namespace ada_mvc.Controllers
{
    public class DetalleDeAsistenciasController : Controller
    {
        private bdEntities db = new bdEntities();

        // GET: DetalleDeAsistencias
        public ActionResult Index()
        {
            var detalleDeAsistencias = db.DetalleDeAsistencias.Include(d => d.Asistencias);
            return View(detalleDeAsistencias.ToList());
        }

        // GET: DetalleDeAsistencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleDeAsistencias detalleDeAsistencias = db.DetalleDeAsistencias.Find(id);
            if (detalleDeAsistencias == null)
            {
                return HttpNotFound();
            }
            return View(detalleDeAsistencias);
        }

        // GET: DetalleDeAsistencias/Create
        public ActionResult Create()
        {
            ViewBag.IdAsistencia = new SelectList(db.Asistencias, "IdAsistencia", "IdAsistencia");
            return View();
        }

        // POST: DetalleDeAsistencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalle,IdAsistencia,FechaHoraEntrada,FechaHoraSalida,Observacion")] DetalleDeAsistencias detalleDeAsistencias)
        {
            if (ModelState.IsValid)
            {
                db.DetalleDeAsistencias.Add(detalleDeAsistencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAsistencia = new SelectList(db.Asistencias, "IdAsistencia", "IdAsistencia", detalleDeAsistencias.IdAsistencia);
            return View(detalleDeAsistencias);
        }

        // GET: DetalleDeAsistencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleDeAsistencias detalleDeAsistencias = db.DetalleDeAsistencias.Find(id);
            if (detalleDeAsistencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAsistencia = new SelectList(db.Asistencias, "IdAsistencia", "IdAsistencia", detalleDeAsistencias.IdAsistencia);
            return View(detalleDeAsistencias);
        }

        // POST: DetalleDeAsistencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalle,IdAsistencia,FechaHoraEntrada,FechaHoraSalida,Observacion")] DetalleDeAsistencias detalleDeAsistencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleDeAsistencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAsistencia = new SelectList(db.Asistencias, "IdAsistencia", "IdAsistencia", detalleDeAsistencias.IdAsistencia);
            return View(detalleDeAsistencias);
        }

        // GET: DetalleDeAsistencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleDeAsistencias detalleDeAsistencias = db.DetalleDeAsistencias.Find(id);
            if (detalleDeAsistencias == null)
            {
                return HttpNotFound();
            }
            return View(detalleDeAsistencias);
        }

        // POST: DetalleDeAsistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleDeAsistencias detalleDeAsistencias = db.DetalleDeAsistencias.Find(id);
            db.DetalleDeAsistencias.Remove(detalleDeAsistencias);
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
