using API.Interfaces;

namespace API.Pagination
{
    public sealed class PaginationHeader : IPaginationHeader
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Items per one page
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Total count of items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; set; }
    }
}
