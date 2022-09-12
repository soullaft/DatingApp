namespace API.DTOs
{
    public sealed class PhotoDto
    {
        public int Id { get; set; }

        public string? Url { get; set; }

        public bool IsMain { get; set; }
    }
}