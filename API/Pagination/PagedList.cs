using Microsoft.EntityFrameworkCore;

namespace API.Pagination
{
    public sealed class PagedList<T> : List<T>, IPagedList
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count">count</param>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        /// <summary>
        /// Execute and pagged <see cref="IQueryable{T}"/> expression
        /// with skip/take results of what we need
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageNumber">number of page</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
