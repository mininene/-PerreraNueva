using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PerreraNueva.Models;
using PerreraNueva.Services.Repository;

namespace PerreraNueva.Controllers
{
    public class EmpleadosController : Controller
    {

        private IGenericRepository<Empleados> _empleadosRepository = null;

        //private PerreraEntities db = new PerreraEntities();


        public EmpleadosController()
        {

            this._empleadosRepository = new GenericRepository<Empleados>();

        }




        

        // GET: Empleados
        public async Task<ActionResult> Index()
        {
            return View(await Task.Run(() => _empleadosRepository.GetAll()));
        }

        // GET: Empleados/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = await Task.Run(() => _empleadosRepository.GetById(id));
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NombreCompleto,Telefono,Correo,DNI")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                _empleadosRepository.Insert(empleados);
                await Task.Run(() => _empleadosRepository.Save());
                return RedirectToAction("Index");
            }

            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = await Task.Run(() => _empleadosRepository.GetById(id));
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NombreCompleto,Telefono,Correo,DNI")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                _empleadosRepository.Insert(empleados);
                _empleadosRepository.Update(empleados);
                await Task.Run(() => _empleadosRepository.Save());
                return RedirectToAction("Index");
            }
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = await Task.Run(() => _empleadosRepository.GetById(id));
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empleados empleados = await Task.Run(() => _empleadosRepository.GetById(id));
            _empleadosRepository.Delete(id);
            await Task.Run(() => _empleadosRepository.Save());
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
