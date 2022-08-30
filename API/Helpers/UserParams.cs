    namespace API.Helpers
{
    /// <summary>
    /// User parametres for viewing <see cref="PagedList{T}"/>
    /// </summary>
    public class UserParams : PaginationParams
    {
        private const int MIN_AGE = 18;
        private const int MAX_AGE = 150;

        public string? CurrentUsername { get; set; }

        public string? Gender { get; set; }

        public int MinAge { get; set; } = MIN_AGE;

        public int MaxAge { get; set; } = MAX_AGE;

        public string OrderBy { get; set; } = "lastActive";
    }
}
