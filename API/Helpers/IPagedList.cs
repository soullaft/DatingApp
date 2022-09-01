namespace API.Helpers
{
    /// <summary>
    /// Default contract for pagedList viewmodel
    /// </summary>
    public interface IPagedList
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}
