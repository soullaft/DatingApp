using API.Pagination;

namespace API.Pagination.Params
{
    public class LikesParams : PaginationParams
    {
        public int UserId { get; set; }

        public string? Predicate { get; set; }
    }
}
