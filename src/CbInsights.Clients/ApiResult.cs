using System;
using System.Collections.Generic;
using System.Text;

namespace CbInsights.Clients
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T ContentObject { get; set; }
        public string Content { get; set; }
    }
}
