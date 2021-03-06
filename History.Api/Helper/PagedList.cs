using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Helper
{
    public class PagedList<ExpandoObject> :List<ExpandoObject>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<ExpandoObject> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }
        public static PagedList<ExpandoObject> ToPagedList(IEnumerable<ExpandoObject> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<ExpandoObject>(items, count, pageNumber, pageSize);
        }
    }
}
