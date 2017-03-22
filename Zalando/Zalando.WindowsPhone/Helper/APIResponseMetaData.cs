using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalando.Network;

namespace Zalando.Helper
{
    public class APIResponseMetaData
    {
        public APIResponseStatus ResponseStatus { get; set; }

        private List<ErrorData> _errors = new List<ErrorData>();
        public List<ErrorData> Errors
        {
            get { return _errors; }
            set { Debug.WriteLine(value); _errors = value; }
        }

        public string Details { get; set; }

        public string Data { get; set; }

        public JToken JsonObj { get; set; }

        public JArray JsonArray { get; set; }

        public int Status { get; set; }

        public static APIResponseMetaData GetDefault()
        {
            return new APIResponseMetaData()
            {
                Data = string.Empty,
                Details = string.Empty,
                JsonArray = null,
                JsonObj = null,
                Status = -1
            };
        }
    }
}