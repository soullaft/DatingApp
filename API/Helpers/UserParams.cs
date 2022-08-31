﻿    namespace API.Helpers
{
    /// <summary>
    /// User parametres for viewing <see cref="PagedList{T}"/>
    /// </summary>
    public class UserParams : PaginationParams
    {

        public string? CurrentUsername { get; set; }

        public string? Gender { get; set; }

        public int MinAge { get; set; } = MsgConst.MIN_AGE;

        public int MaxAge { get; set; } = MsgConst.MAX_AGE;

        public string OrderBy { get; set; } = "lastActive";
    }
}
