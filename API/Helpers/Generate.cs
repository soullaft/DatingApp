using API.Interfaces;

namespace API.Helpers
{
    //todo: rework
    public static class Generate<T> where T : class, new()
    {
        public static IPaginationHeader GenerateHeaderParams(PagedList<T> headerParams)
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
