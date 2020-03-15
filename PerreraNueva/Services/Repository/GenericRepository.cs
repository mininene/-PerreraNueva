using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PerreraNueva.Models;

namespace PerreraNueva.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        public PerreraEntities _context = null;  //DB context para base externa en guia
        public DbSet<T> table = null;


        public GenericRepository()
        {
            this._context = new PerreraEntities();
            table = _context.Set<T>();
        }
        public GenericRepository(PerreraEntities _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }


        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}