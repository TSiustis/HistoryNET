using History.Api.Data;
using History.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Services
{
    public class GenericRepository<T> where T : BaseModel

    {
        internal HistoryDbContext _context;
        internal DbSet<T> dbSet;
        public GenericRepository(HistoryDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();

        }

        public IEnumerable<T> GetAllForDay(string Day)
        {
           
            return dbSet.Where(e => e.Day.Equals(Day)).Include(e => e.Link);
        }
        public IEnumerable<T> GetAllForYear(string Year)
        {

            return dbSet.Where(e => e.Year.Equals(Year)).Include(e => e.Link);
        }
        public IEnumerable<T> GetAllForDayAndYear(string Year,string Day)
        {

            return dbSet.Where(e => e.Day.Equals(Day) && e.Year.Equals(Year)).Include(e => e.Link);
        }

        public List<Link> GetLinksByModelId(int modelId)
        {
            return dbSet.Where(e => e.Id == modelId).FirstOrDefault().Link;
        }
        public T GetById(int id)
        {
            return dbSet.Where(e => e.Id == id).Include(e=>e.Link).Single();
        } 

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public  void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public bool Exists(int id)
        {
            return dbSet.Any(e => e.Id == id);
        }

    }
}
