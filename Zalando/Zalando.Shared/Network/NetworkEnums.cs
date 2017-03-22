using System;
using System.Collections.Generic;
using System.Text;

namespace Zalando.Network
{
    public enum RequestType
    {
        GET = 1,
        POST = 2
    }

    public enum ResponseType
    {
        TEXT = 1,
        STREAM = 2
    }

    public enum WebResponseStatus
    {
        Success = 1,
        ServerError = 2,
        ConnectionError = 4
    }

    public enum JsonResponseType
    {
        JObject = 1,
        JArray = 2
    }

    public enum APIResponseStatus
    {
        Success = 1,
        UnknownError = 2,
        JsonParsingError = 3,
        APIError = 4,
        KnownError = 5,
        ServerError = 6,
        ConnectionError = 7
    }
}