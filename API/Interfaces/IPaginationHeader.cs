namespace API.Interfaces
{
    /// <summary>
    /// Pagination header which is contain in the header of http response of a client
    /// </summary>
    public interface IPaginationHeader
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Items per page
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Total items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }
    }
}