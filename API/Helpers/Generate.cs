using API.Interfaces;
using API.Pagination;

namespace API.Helpers
{
    //todo: rework
    public static class GenerateHeader
    {
        /// <summary>
        /// Generate header depends on <see cref="IPagedList"/> information
        /// </summary>
        /// <param name="headerParams"></param>
        /// <returns></returns>
        public static IPaginationHeader GenerateHeaderParams(IPagedList headerParams)
        {
            var instance = new PaginationHeader
            {
                CurrentPage  = headerParams.CurrentPage,
                ItemsPerPage = headerParams.PageSize,
                TotalItems   = headerParams.TotalCount,
                TotalPages   = headerParams.TotalPages
            };

            return instance;
        }
    }
}
