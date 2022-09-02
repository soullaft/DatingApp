namespace API.Pagination
{
    public class PaginationParams
    {
        protected const int MAX_PAGE_SIZE = 50;
        protected int _pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value; }
        }
    }
}
