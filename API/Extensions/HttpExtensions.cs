﻿using API.Helpers;
using API.Interfaces;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, IPaginationHeader paginationHeader)
        {
            if(paginationHeader is null) throw new ArgumentNullException(nameof(paginationHeader));

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
