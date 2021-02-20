using History.Api.Helper;
using History.Shared.Models;
using System.Collections.Generic;

namespace History.Api.Services
{
    public interface IGenericRepository<T> where T : TypeOfEvent
    {
        void Delete(T entityToDelete);
        bool Exists(int id);
        PagedList<T> GetAllForDay(string Day, QueryParameters queryParameters);
        PagedList<T> GetAllForDayAndYear(string Year, string Day, QueryParameters queryParameters);
        PagedList<T> GetAllForYear(string Year, QueryParameters queryParameters);
        T GetById(int id);
        List<Link> GetLinksByModelId(int modelId);
        void Insert(T entity);
        void Update(T entity);
    }
}