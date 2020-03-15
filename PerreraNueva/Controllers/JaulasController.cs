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
    public class JaulasController : Controller
    {
        //private PerreraEntities db = new PerreraEntities();


        private IGenericRepository<Jaulas> _jaulasRepository = null;


        
        public JaulasController()
        {
            
            this._jaulasRepository = new GenericRepository<Jaulas>();

        }






        // GET: Jaulas
        public async Task<ActionResult> Index()
        {
            return View(await Task.Run(() => _jaulasRepository.GetAll()));
        }

        // GET: Jaulas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jaulas jaulas = await Task.Run(() => _jaulasRepository.GetById(id));
            if (jaulas == null)
            {
                return HttpNotFound();
            }
            return View(jaulas);
        }

        // GET: Jaulas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jaulas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NombreJaula")] Jaulas jaulas)
        {
            if (ModelState.IsValid)
            {
                _jaulasRepository.Insert(jaulas);
                await Task.Run(() => _jaulasRepository.Save());
                return RedirectToAction("Index");
            }

            return View(jaulas);
        }

        // GET: Jaulas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jaulas jaulas = await Task.Run(() => _jaulasRepository.GetById(id));
            if (jaulas == null)
            {
                return HttpNotFound();
            }
            return View(jaulas);
        }

        // POST: Jaulas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NombreJaula")] Jaulas jaulas)
        {
            if (ModelState.IsValid)
            {
                _jaulasRepository.Insert(jaulas);
                _jaulasRepository.Update(jaulas);
                await Task.Run(() => _jaulasRepository.Save());
                return RedirectToAction("Index");
            }
            return View(jaulas);
        }

        // GET: Jaulas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jaulas jaulas = await Task.Run(() => _jaulasRepository.GetById(id));
            if (jaulas == null)
            {
                return HttpNotFound();
            }
            return View(jaulas);
        }

        // POST: Jaulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Jaulas jaulas = await Task.Run(() => _jaulasRepository.GetById(id));
            _jaulasRepository.Delete(id);
            await Task.Run(() => _jaulasRepository.Save());
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
