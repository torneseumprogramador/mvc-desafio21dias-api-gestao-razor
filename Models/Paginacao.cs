using System;
using System.Collections.Generic;

namespace web_renderizacao_server_side.Models
{
    public record Paginacao<T>
    {
        public List<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}
