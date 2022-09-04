using API.Helpers;
using API.Interfaces;
using System.Text.Json;

namespace API.Extensions
{
    /// <summary>
    /// Http extensions
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Add pagination header to a page
        /// </summary>
        /// <param name="response"></param>
        /// <param name="paginationHeader"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddPaginationHeader(this HttpResponse response, IPaginationHeader paginationHeader)
        {
            if(paginationHeader is null) throw new ArgumentNullException(nameof(paginationHeader));

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            //add necessary information to a header for pagination
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
