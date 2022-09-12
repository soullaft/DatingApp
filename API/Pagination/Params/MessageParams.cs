using API.Helpers;
using API.Pagination;

namespace API.Pagination.Params
{
    public sealed class MessageParams : PaginationParams
    {
        public string? Username { get; set; }

        public string Container { get; set; } = MsgConst.MESSAGE_UNREAD;
    }
}
