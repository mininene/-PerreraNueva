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
    public class PerrosController : Controller
    {
        private IGenericRepository<Perros> _perrosRepository = null;
        private IGenericRepository<Jaulas> _jaulasRepository = null;
        private IGenericRepository<Razas> _razasRepository = null;

      //  private PerreraEntities db = new PerreraEntities();


        public PerrosController()
        {
           
            this._perrosRepository = new GenericRepository<Perros>();
            this._jaulasRepository = new GenericRepository<Jaulas>();
            this._razasRepository = new GenericRepository<Razas>();

        }

        // GET: Perros
        public async Task<ActionResult> Index()
        {
            //var perros = db.Perros.Include(p => p.Jaulas).Include(p => p.Razas);
            //return View(await perros.ToListAsync());
            var model = await Task.Run(() => _perrosRepository.GetAll());
            return View(model.ToList());
        }

        // GET: Perros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perros perros = await Task.Run(() => _perrosRepository.GetById(id));
            if (perros == null)
            {
                return HttpNotFound();
            }
            return View(perros);
        }

        // GET: Perros/Create
        public ActionResult Create()
        {
            ViewBag.IdJaula = new SelectList(_jaulasRepository.GetAll(), "Id", "NombreJaula");
            ViewBag.CodRazaId = new SelectList(_razasRepository.GetAll(), "Id", "Nombre");
            return View();
        }

        // POST: Perros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Chip,FechaNacimiento,CodRazaId,IdJaula")] Perros perros)
        {
            if (ModelState.IsValid)
            {
                _perrosRepository.Insert(perros);
                await Task.Run(() => _perrosRepository.Save());
                return RedirectToAction("Index");
            }

            ViewBag.IdJaula = new SelectList(_jaulasRepository.GetAll(), "Id", "NombreJaula", perros.IdJaula);
            ViewBag.CodRazaId = new SelectList(_razasRepository.GetAll(), "Id", "Nombre", perros.CodRazaId);
            return View(perros);
        }

        // GET: Perros/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perros perros = await Task.Run(() => _perrosRepository.GetById(id));
            if (perros == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdJaula = new SelectList(_jaulasRepository.GetAll(), "Id", "NombreJaula", perros.IdJaula);
            ViewBag.CodRazaId = new SelectList(_razasRepository.GetAll(), "Id", "Nombre", perros.CodRazaId);
            return View(perros);
        }

        // POST: Perros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Chip,FechaNacimiento,CodRazaId,IdJaula")] Perros perros)
        {
            if (ModelState.IsValid)
            {
                _perrosRepository.Insert(perros);
                _perrosRepository.Update(perros);
                await Task.Run(() => _perrosRepository.Save());
                return RedirectToAction("Index");
            }
            ViewBag.IdJaula = new SelectList(_jaulasRepository.GetAll(), "Id", "NombreJaula", perros.IdJaula);
            ViewBag.CodRazaId = new SelectList(_razasRepository.GetAll(), "Id", "Nombre", perros.CodRazaId);
            return View(perros);
        }

        // GET: Perros/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perros perros = await Task.Run(() => _perrosRepository.GetById(id));
            if (perros == null)
            {
                return HttpNotFound();
            }
            return View(perros);
        }

        // POST: Perros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Perros perros = await Task.Run(() => _perrosRepository.GetById(id));
            _perrosRepository.Delete(id);
            await Task.Run(() => _perrosRepository.Save());
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
