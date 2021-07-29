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
    public class EquiposTecnologicosController : Controller
    {
        private bdEntities db = new bdEntities();

        // GET: EquiposTecnologicos
        public ActionResult Index()
        {
            var equiposTecnologicos = db.EquiposTecnologicos.Include(e => e.Empleados).Include(e => e.Usuarios);
            return View(equiposTecnologicos.ToList());
        }

        // GET: EquiposTecnologicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquiposTecnologicos equiposTecnologicos = db.EquiposTecnologicos.Find(id);
            if (equiposTecnologicos == null)
            {
                return HttpNotFound();
            }
            return View(equiposTecnologicos);
        }

        // GET: EquiposTecnologicos/Create
        public ActionResult Create()
        {
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre");
            ViewBag.CreadoPorUserName = new SelectList(db.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: EquiposTecnologicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEquipo,Descripcion,Modelo,Marca,NumeroDeSerie,CodigoInterno,Estado,CodEmpleado,ValorMonetario,CreadoPorUserName,Observacion")] EquiposTecnologicos equiposTecnologicos)
        {
            if (ModelState.IsValid)
            {
                db.EquiposTecnologicos.Add(equiposTecnologicos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre", equiposTecnologicos.CodEmpleado);
            ViewBag.CreadoPorUserName = new SelectList(db.Usuarios, "IdUsuario", "Nombre", equiposTecnologicos.CreadoPorUserName);
            return View(equiposTecnologicos);
        }

        // GET: EquiposTecnologicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquiposTecnologicos equiposTecnologicos = db.EquiposTecnologicos.Find(id);
            if (equiposTecnologicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre", equiposTecnologicos.CodEmpleado);
            ViewBag.CreadoPorUserName = new SelectList(db.Usuarios, "IdUsuario", "Nombre", equiposTecnologicos.CreadoPorUserName);
            return View(equiposTecnologicos);
        }

        // POST: EquiposTecnologicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEquipo,Descripcion,Modelo,Marca,NumeroDeSerie,CodigoInterno,Estado,CodEmpleado,ValorMonetario,CreadoPorUserName,Observacion")] EquiposTecnologicos equiposTecnologicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equiposTecnologicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEmpleado = new SelectList(db.Empleados, "CodEmpleado", "Nombre", equiposTecnologicos.CodEmpleado);
            ViewBag.CreadoPorUserName = new SelectList(db.Usuarios, "IdUsuario", "Nombre", equiposTecnologicos.CreadoPorUserName);
            return View(equiposTecnologicos);
        }

        // GET: EquiposTecnologicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquiposTecnologicos equiposTecnologicos = db.EquiposTecnologicos.Find(id);
            if (equiposTecnologicos == null)
            {
                return HttpNotFound();
            }
            return View(equiposTecnologicos);
        }

        // POST: EquiposTecnologicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquiposTecnologicos equiposTecnologicos = db.EquiposTecnologicos.Find(id);
            db.EquiposTecnologicos.Remove(equiposTecnologicos);
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
