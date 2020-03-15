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
    public class ClientesController : Controller
    {
        //private PerreraEntities db = new PerreraEntities();
       // private IClientesRepository _clientesRepository = null;
       private IGenericRepository<Clientes> _clientesRepository = null;


        // GET: Clientes
        public ClientesController()
        {
            //this._clientesRepository = new ClientesRepository() ;
            this._clientesRepository = new GenericRepository<Clientes>();

        }

     

     


        public async Task<ActionResult> Index()
        {

            var model = await Task.Run(()=>_clientesRepository.GetAll());
            return View( model.ToList());
        }

        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = await Task.Run(() => _clientesRepository.GetById(id));
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NombreCompleto,Telefono,Correo,DNI")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _clientesRepository.Insert(clientes);
                await Task.Run(() => _clientesRepository.Save());
                return RedirectToAction("Index");
            }

            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = await Task.Run(() => _clientesRepository.GetById(id));
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NombreCompleto,Telefono,Correo,DNI")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _clientesRepository.Insert(clientes);
                _clientesRepository.Update(clientes);
                await Task.Run(() => _clientesRepository.Save());
                return RedirectToAction("Index");
            }
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = await Task.Run(() => _clientesRepository.GetById(id));
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clientes clientes = await Task.Run(() => _clientesRepository.GetById(id));
            _clientesRepository.Delete(id);
            await Task.Run(() => _clientesRepository.Save());
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
