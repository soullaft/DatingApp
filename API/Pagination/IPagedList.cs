namespace API.Pagination
{
    /// <summary>
    /// Default contract for pagedList viewmodel
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Page size (elements per on page)
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total count of elements
        /// </summary>
        public int TotalCount { get; set; }
    }
}
