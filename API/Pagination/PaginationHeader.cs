using API.Interfaces;

namespace API.Pagination
{
    public class PaginationHeader : IPaginationHeader
    {
        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
