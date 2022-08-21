namespace API.Helpers
{
    /// <summary>
    /// User parametres for viewing <see cref="PagedList{T}"/>
    /// </summary>
    public class UserParams
    {
        private const int MAX_PAGE_SIZE = 50;
        private const int MIN_AGE = 18;
        private const int MAX_AGE = 150;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public string? CurrentUsername { get; set; }

        public string? Gender { get; set; }

        public int MinAge { get; set; } = MIN_AGE;

        public int MaxAge { get; set; } = MAX_AGE;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value; }
        }
    }
}
