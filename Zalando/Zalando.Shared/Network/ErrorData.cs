using System;
using System.Collections.Generic;
using System.Text;

namespace Zalando.Network
{
    public class ErrorData
    {
        public ErrorData()
        {
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
        }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
