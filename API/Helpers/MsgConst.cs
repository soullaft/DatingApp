namespace API.Helpers
{
    public static class MsgConst
    {
        public const string LIKE_PREDICATE = "liked";
        public const string LIKED_BY_PREDICATE = "likedBy";
        
        public const string MESSAGE_UNREAD = "Unread";
        public const string MESSAGE_INBOX = "Inbox";
        public const string MESSAGE_OUTBOX = "Outbox";
        
        public const int MIN_AGE = 18;
        public const int MAX_AGE = 150;

        public const string RESOURCE_CONNECTION_SETTINGS = "DefaultConnection";
        public const string RESOURCE_CLOUDINARY_SETTINGS = "CloudinarySettings";

        public const int IMAGE_DEFAULT_HEIGHT = 500;
        public const int IMAGE_DEFAULT_WIDTH = 500;

        public const string ORDER_BY_LAST_ACTIVE = "lastActive";
        public const string ORDER_BY_LAST_CREATED = "created";
    }
}
