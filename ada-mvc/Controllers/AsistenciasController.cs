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
    public class AsistenciasController : Controller
    {
        private bdEntities db = new bdEntities();

        // GET: Asistencias
        public ActionResult Index()
        {
            var asistencias = db.Asistencias.Include(a => a.Empleados);
            return View(asistencias.ToList());
        }

        // GET: Asistencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // GET: Asistencias/Create
        public ActionResult Create()
        {
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre");
            return View();
        }

        // POST: Asistencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAsistencia,CodEmpleado")] Asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.Asistencias.Add(asistencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre", asistencias.CodEmpleado);
            return View(asistencias);
        }

        // GET: Asistencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre", asistencias.CodEmpleado);
            return View(asistencias);
        }

        // POST: Asistencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAsistencia,CodEmpleado")] Asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre", asistencias.CodEmpleado);
            return View(asistencias);
        }

        // GET: Asistencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asistencias asistencias = db.Asistencias.Find(id);
            db.Asistencias.Remove(asistencias);
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
