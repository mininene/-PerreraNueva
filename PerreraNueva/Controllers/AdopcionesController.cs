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
    public class AdopcionesController : Controller
    {
        //private PerreraEntities db = new PerreraEntities();

        private IGenericRepository<Adopciones> _adopcionesRepository = null;
        private IGenericRepository<Clientes> _clientesRepository = null;
        private IGenericRepository<Empleados> _empleadosRepository = null;
        private IGenericRepository<Perros> _perrosRepository = null;


        public AdopcionesController()
        {

            this._adopcionesRepository = new GenericRepository<Adopciones>();
            this._clientesRepository = new GenericRepository<Clientes>();
            this._empleadosRepository = new GenericRepository<Empleados>();
            this._perrosRepository = new GenericRepository<Perros>();

        }




        // GET: Adopciones
        public async Task<ActionResult> Index()
        {
            //var adopciones = db.Adopciones.Include(a => a.Clientes).Include(a => a.Empleados).Include(a => a.Perros);
            //return View(await adopciones.ToListAsync());

            var model = await Task.Run(() => _adopcionesRepository.GetAll());
            return View(model.ToList());
        }

        //GET: Adopciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest) ;
            }
            Adopciones adopciones = await Task.Run(() => _adopcionesRepository.GetById(id));
            if (adopciones == null)
            {
                return HttpNotFound();
            }
            return View(adopciones);
        }

        // GET: Adopciones/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_clientesRepository.GetAll(), "Id", "NombreCompleto");
            ViewBag.EmpleadoId = new SelectList(_empleadosRepository.GetAll(), "Id", "NombreCompleto");
            ViewBag.PerroId = new SelectList(_perrosRepository.GetAll(), "Id", "Nombre");
            return View();
        }

        // POST: Adopciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PerroId,ClienteId,EmpleadoId,FechaEntrega")] Adopciones adopciones)
        {
            if (ModelState.IsValid)
            {
                _adopcionesRepository.Insert(adopciones);
                await Task.Run(() => _adopcionesRepository.Save());
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(_clientesRepository.GetAll(), "Id", "NombreCompleto", adopciones.ClienteId);
            ViewBag.EmpleadoId = new SelectList(_empleadosRepository.GetAll(), "Id", "NombreCompleto", adopciones.EmpleadoId);
            ViewBag.PerroId = new SelectList(_perrosRepository.GetAll(), "Id", "Nombre", adopciones.PerroId);
            return View(adopciones);
        }

        // GET: Adopciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Adopciones adopciones = await Task.Run(() => _adopcionesRepository.GetById(id));
            if (adopciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(_clientesRepository.GetAll(), "Id", "NombreCompleto", adopciones.ClienteId);
            ViewBag.EmpleadoId = new SelectList(_empleadosRepository.GetAll(), "Id", "NombreCompleto", adopciones.EmpleadoId);
            ViewBag.PerroId = new SelectList(_perrosRepository.GetAll(), "Id", "Nombre", adopciones.PerroId);
            return View(adopciones);
        }

        // POST: Adopciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PerroId,ClienteId,EmpleadoId,FechaEntrega")] Adopciones adopciones)
        {
            if (ModelState.IsValid)
            {
                _adopcionesRepository.Insert(adopciones);
                _adopcionesRepository.Update(adopciones);
                await Task.Run(() => _adopcionesRepository.Save());
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(_clientesRepository.GetAll(), "Id", "NombreCompleto", adopciones.ClienteId);
            ViewBag.EmpleadoId = new SelectList(_empleadosRepository.GetAll(), "Id", "NombreCompleto", adopciones.EmpleadoId);
            ViewBag.PerroId = new SelectList(_perrosRepository.GetAll(), "Id", "Nombre", adopciones.PerroId);
            return View(adopciones);
        }

        // GET: Adopciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopciones adopciones = await Task.Run(()=> _adopcionesRepository.GetById(id));
            if (adopciones == null)
            {
                return HttpNotFound();
            }
            return View(adopciones);
        }

        // POST: Adopciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Adopciones adopciones = await Task.Run(() => _adopcionesRepository.GetById(id));
            _adopcionesRepository.Delete(id);
            await Task.Run(() => _adopcionesRepository.Save());
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
