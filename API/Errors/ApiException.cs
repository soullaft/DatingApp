namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }

        //todo: make api exception depends on severity for further showing this logs on desktop log viewer
        public Severity Severity { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }

    public enum Severity
    {
        Information,
        Warning,
        Error,
    }
}
