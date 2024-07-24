using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWork
{
    public class Pagination<T> : PaginationBase where T : class
    {
        public List<T> Items { get; set; }
        public Pagination(int pageIndex, int pageSize, int totalRecords, List<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Items = items;
        }
    }
}
