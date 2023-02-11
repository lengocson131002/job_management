using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private JobManagementDBContext _context = null;
        private DbSet<T> table = null;

        public BaseRepository(JobManagementDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            if (id == null)
            {
                return null;
            }

            return table.Find(id);
        }

        public void Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }

            table.Add(obj);
            Save();
        }

        public void Update(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }

            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Id");
            }

            T existing = table.Find(id);
            if (existing == null)
            {
                return;
            }

            table.Remove(existing);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
