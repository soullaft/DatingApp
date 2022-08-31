namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public string? SenderUsername { get; set; }

        public AppUser? Sender { get; set; }

        public int RecipientId { get; set; }

        public int RecipientUsername { get; set; }

        public AppUser? Recipient { get; set; }
    }
}
