using System;

namespace Movimientos.Api.Utils
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public String Message { get; set; }
        public Object Data { get; set; }
    }
}
