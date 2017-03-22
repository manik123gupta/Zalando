using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalando.Network;

namespace Zalando.Helper
{
    public static class DataHelper
    {
        public static APIResponseMetaData GetResponseObject(string json)
        {
            JObject jsonObj = null;
            try
            {
                jsonObj = ((JObject)JsonConvert.DeserializeObject(json));
            }
            catch
            {
                jsonObj = null;
            }

            return GetObject(jsonObj, json);
        }

        public static APIResponseMetaData GetResponseObject(RequestManagerResponse response)
        {
            if (response.WebResponseStatus == WebResponseStatus.ServerError)
            {
                return new APIResponseMetaData()
                {
                    ResponseStatus = APIResponseStatus.ServerError,
                };
            }
            else if (response.WebResponseStatus == WebResponseStatus.ConnectionError)
            {
                return new APIResponseMetaData()
                {
                    ResponseStatus = APIResponseStatus.ConnectionError,
                };
            }

            JObject jsonObj = null;
            try
            {
                jsonObj = ((JObject)JsonConvert.DeserializeObject(response.Text));

            }
            catch (Exception e)
            {
                jsonObj = null;
            }

            return GetObject(jsonObj, response.Text);
        }

        public static APIResponseMetaData GetResponseObjectWithErrors(string json)
        {
            JObject jsonObj = null;
            try
            {
                jsonObj = ((JObject)JsonConvert.DeserializeObject(json));
            }
            catch
            {
                jsonObj = null;
            }

            return GetObject(jsonObj, json, true);
        }

        public static APIResponseMetaData GetResponseObjectWithErrors(RequestManagerResponse response)
        {
            if (response.WebResponseStatus == WebResponseStatus.ServerError)
            {
                return new APIResponseMetaData()
                {
                    ResponseStatus = APIResponseStatus.ServerError,
                };
            }
            else if (response.WebResponseStatus == WebResponseStatus.ConnectionError)
            {
                return new APIResponseMetaData()
                {
                    ResponseStatus = APIResponseStatus.ConnectionError,
                };
            }

            JObject jsonObj = null;
            try
            {
                jsonObj = ((JObject)JsonConvert.DeserializeObject(response.Text));
            }
            catch
            {
                jsonObj = null;
            }

            return GetObject(jsonObj, response.Text, true);
        }

        private static APIResponseMetaData GetObject(JToken jsonObj, string json, bool accepErrors = false)
        {

            if (jsonObj != null) //if json is correct
            {
                if (jsonObj.GetObject("errors", null) == null) //if error object does not exists, it is successfull.
                {
                    return new APIResponseMetaData()
                    {
                        ResponseStatus = APIResponseStatus.Success,
                        Data = json,
                        JsonObj = jsonObj,
                    };
                }
                else //error object exists.
                {
                    if (accepErrors)
                    {
                        return new APIResponseMetaData()
                        {
                            ResponseStatus = APIResponseStatus.Success,
                            Data = json,
                            JsonObj = jsonObj,
                            Errors = jsonObj.GetObject("errors").Select(error => new ErrorData()
                            {
                                ErrorCode = error.GetString("errorCode"),
                                ErrorMessage = error.GetString("errorMessage")
                            }).ToList(),
                        };
                    }
                    else
                    {
                        return new APIResponseMetaData()
                        {
                            ResponseStatus = APIResponseStatus.APIError,
                            Errors = jsonObj.GetObject("errors").Select(error => new ErrorData()
                            {
                                ErrorCode = error.GetString("errorCode"),
                                ErrorMessage = error.GetString("errorMessage")
                            }).ToList(),
                            Data = json,
                        };
                    }
                }
            }
            else //if json is incorrect
            {
                return new APIResponseMetaData()
                {
                    ResponseStatus = APIResponseStatus.JsonParsingError,
                    Data = json
                };
            }
        }

        public static APIResponseMetaData GetResponseObject(string json, JsonResponseType jsonResponseType = JsonResponseType.JObject)
        {
            switch (jsonResponseType)
            {
                case JsonResponseType.JObject:
                    JObject jsonObj = null;
                    try
                    {
                        jsonObj = ((JObject)JsonConvert.DeserializeObject(json));
                    }
                    catch
                    {
                        jsonObj = null;
                    }

                    if (jsonObj != null) //if json is correct
                    {
                        if (jsonObj.GetString("status") == "0") //if successfull
                        {
                            return new APIResponseMetaData()
                            {
                                ResponseStatus = APIResponseStatus.Success,
                                Data = json,
                                JsonObj = jsonObj
                            };
                        }
                        else if (jsonObj.GetString("status") == "1") //if not successfull
                        {
                            return new APIResponseMetaData()
                            {
                                ResponseStatus = APIResponseStatus.UnknownError,
                                //ErrorCode = jsonObj.GetInt("errorcode"),
                                Data = json
                            };
                        }
                        else
                        {
                            return new APIResponseMetaData()
                            {
                                ResponseStatus = APIResponseStatus.UnknownError,
                                //ErrorCode = jsonObj.GetInt("errorcode"),
                                Data = json
                            };
                        }
                    }
                    else //if json is incorrect
                    {
                        return new APIResponseMetaData()
                        {
                            ResponseStatus = APIResponseStatus.JsonParsingError,
                            Data = json
                        };
                    }

                case JsonResponseType.JArray:
                    JArray jsonArray = null;
                    try
                    {
                        jsonArray = ((JArray)JsonConvert.DeserializeObject(json));
                    }
                    catch
                    {
                        jsonArray = null;
                    }

                    return GetObject(jsonArray, json);

                default:
                    return null;
            }
        }
    }
}