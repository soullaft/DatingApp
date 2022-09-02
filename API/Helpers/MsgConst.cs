﻿namespace API.Helpers
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
    }
}
