namespace API.DTOs
{
    public sealed class CreateMessageDto
    {
        public string? RecipientUsername { get; set; }

        public string? Content { get; set; }
    }
}
