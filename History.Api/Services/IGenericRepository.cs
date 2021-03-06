using History.Api.Helper;
using History.Shared.Models;
using System.Collections.Generic;
using System.Dynamic;

namespace History.Api.Services
{
    public interface IGenericRepository<T> where T : TypeOfEvent
    {
        void Delete(T entityToDelete);
        bool Exists(int id);
        PagedList<ExpandoObject> GetAllForDay(string Day, QueryParameters queryParameters);
        PagedList<ExpandoObject> GetAllForDayAndYear(string Year, string Day, QueryParameters queryParameters);
        PagedList<ExpandoObject> GetAllForYear(string Year, QueryParameters queryParameters);
        ExpandoObject GetById(int id, string fields);
        T GetById(int id);
        List<Link> GetLinksByModelId(int modelId);
        void Insert(T entity);
        void Update(T entity);
    }
}