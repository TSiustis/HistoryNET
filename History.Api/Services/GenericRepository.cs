using History.Api.Data;
using History.Api.Helper;
using History.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : TypeOfEvent

    {
        private readonly HistoryDbContext _context;
        private readonly DbSet<T> dbSet;
        private readonly IDataShaper<T> _dataShaper;
        private HistoryDbContext @object;

        public GenericRepository()
        {

        }
       
        public GenericRepository(HistoryDbContext context, IDataShaper<T> dataShaper)
        {
            _context = context;
            dbSet = _context.Set<T>();
            _dataShaper = dataShaper;

        }

        public GenericRepository(HistoryDbContext @object)
        {
            this.@object = @object;
        }

        public PagedList<ExpandoObject> GetAllForDay(string Day, QueryParameters queryParameters)
        {
            var result = dbSet.Where(e => e.Day.Equals(Day)).Include(e => e.Link);
            var shapedResult = _dataShaper.ShapeData(result, queryParameters.Fields);
            return PagedList<ExpandoObject>.ToPagedList(shapedResult, queryParameters.PageNumber, queryParameters.PageSize) ;
        }
        public PagedList<ExpandoObject> GetAllForYear(string Year, QueryParameters queryParameters)
        {
            var result = dbSet.Where(e => e.Year.Equals(Year)).Include(e => e.Link);
            var shapedResult = _dataShaper.ShapeData(result, queryParameters.Fields);

            return PagedList<ExpandoObject>.ToPagedList(shapedResult , queryParameters.PageNumber, queryParameters.PageSize);
        }
        public PagedList<ExpandoObject> GetAllForDayAndYear(string Year, string Day, QueryParameters queryParameters)
        {
            var result = dbSet.Where(e => e.Day.Equals(Day) && e.Year.Equals(Year)).Include(e => e.Link);
            var shapedResult = _dataShaper.ShapeData(result, queryParameters.Fields);

            return PagedList<ExpandoObject>.ToPagedList(shapedResult
                                        , queryParameters.PageNumber, queryParameters.PageSize);
        }

        public List<Link> GetLinksByModelId(int modelId)
        {
            return dbSet.Where(e => e.Id == modelId).Include(e=>e.Link).FirstOrDefault().Link;
        }
        public virtual T GetById(int id)
        {
            return dbSet.Where(e => e.Id == id).Include(e=>e.Link).SingleOrDefault();
        }
        public  ExpandoObject GetById(int id, string fields)
        {
            var result = dbSet.Where(e => e.Id == id).Include(e=>e.Link).SingleOrDefault();
            return _dataShaper.ShapeData(result, fields);
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
        public void Delete(T entityToDelete)
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
