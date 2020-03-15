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
    public class RazasController : Controller
    {

        private IGenericRepository<Razas> _razasRepository = null;

        //private PerreraEntities db = new PerreraEntities();


        public RazasController()
        {

            this._razasRepository = new GenericRepository<Razas>();

        }
       



        // GET: Razas
        public async Task<ActionResult> Index()
        {
            return View(await Task.Run(() => _razasRepository.GetAll()));
        }

        // GET: Razas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Razas razas = await Task.Run(() => _razasRepository.GetById(id));
            if (razas == null)
            {
                return HttpNotFound();
            }
            return View(razas);
        }

        // GET: Razas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Razas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre")] Razas razas)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _razasRepository.Insert(razas));
                await Task.Run(() => _razasRepository.Save());
                return RedirectToAction("Index");
            }

            return View(razas);
        }

        // GET: Razas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Razas razas = await Task.Run(() => _razasRepository.GetById(id));
            if (razas == null)
            {
                return HttpNotFound();
            }
            return View(razas);
        }

        // POST: Razas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre")] Razas razas)
        {
            if (ModelState.IsValid)
            {
                _razasRepository.Insert(razas);
                _razasRepository.Update(razas);
                await Task.Run(() => _razasRepository.Save());
                return RedirectToAction("Index");
            }
            return View(razas);
        }

        // GET: Razas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Razas razas = await Task.Run(() => _razasRepository.GetById(id));
            if (razas == null)
            {
                return HttpNotFound();
            }
            return View(razas);
        }

        // POST: Razas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Razas razas = await Task.Run(() => _razasRepository.GetById(id));
            _razasRepository.Delete(id);
            await Task.Run(() => _razasRepository.Save());
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
