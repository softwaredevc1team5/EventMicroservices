using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.ViewModels
{
    public class PaginatedWishListViewModel
    <TEntity>
        where TEntity : class
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public long Count { get; private set; }
        public IEnumerable<TEntity> Data { get; set; }

        public PaginatedWishListViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Count = count;
            this.Data = data;
        }
    }
}
