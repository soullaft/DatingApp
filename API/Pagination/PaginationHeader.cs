using API.Interfaces;

namespace API.Pagination
{
    public sealed class PaginationHeader : IPaginationHeader
    {
        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
