using System;
using System.Collections.Generic;

using System.Text;
using Windows.Web.Http;

namespace Zalando.Network
{
    public class BasicNetwork
    {
        public string Url { get; set; }

        public delegate void RequestCompleted(object sender, RequestManagerResponse response);
        public event RequestCompleted RequestCompletedEvent;

        private Dictionary<string, object> _responseHeaders = new Dictionary<string, object>();
        public Dictionary<string, object> ResponseHeaders
        {
            get { return _responseHeaders; }
            set { _responseHeaders = value; }
        }

        private RequestType _requestType = RequestType.GET;
        private ResponseType _responseType = ResponseType.TEXT;
        private string _url = string.Empty;
        private string _postBody = string.Empty;

        HttpClient httpClient;
        HttpRequestMessage httpRequestMessage;

        public async void MakeRequest()
        {
            HttpResponseMessage response = null;
            Uri stringUri = new Uri(Url);
            httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, stringUri);
            httpClient = new HttpClient();

            try
            {
                if (_requestType == RequestType.GET)
                    response = await httpClient.SendRequestAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                {
                    if (_responseType == ResponseType.STREAM)
                    {
                        if (RequestCompletedEvent != null)
                        {
                            RequestCompletedEvent(httpRequestMessage, new RequestManagerResponse()
                            {
                                WebResponseStatus = WebResponseStatus.Success,
                                Text = null,
                                Stream = await response.Content.ReadAsInputStreamAsync()
                            });
                        }
                        return;
                    }
                    else if (_responseType == ResponseType.TEXT)
                    {
                        RequestCompletedEvent(httpRequestMessage, new RequestManagerResponse()
                        {
                            WebResponseStatus = WebResponseStatus.Success,
                            Text = await response.Content.ReadAsStringAsync(),
                            Stream = null
                        });

                        return;
                    }
                }
                else
                {
                    if (RequestCompletedEvent != null)
                    {
                        RequestCompletedEvent(WebResponseStatus.ServerError, new RequestManagerResponse()
                        {
                            WebResponseStatus = WebResponseStatus.ServerError,
                            Text = null,
                            Stream = null
                        });
                    }
                }
            }
            catch (Exception e)
            {
                if (RequestCompletedEvent != null)
                {
                    RequestCompletedEvent(WebResponseStatus.ServerError, new RequestManagerResponse()
                    {
                        WebResponseStatus = WebResponseStatus.ServerError,
                        Text = null,
                        Stream = null,
                        Error = new ErrorData() { ErrorMessage = e.Message }
                    });
                }
            }
        }

        public async void MakeRequest(Action<object, RequestManagerResponse> responseCallback)
        {
            HttpResponseMessage response = null;
            Uri stringUri = new Uri(Url);
            httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, stringUri);
            httpClient = new HttpClient();
            try
            {
                if (_requestType == RequestType.GET)
                    response = await httpClient.SendRequestAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                {
                    if (_responseType == ResponseType.STREAM)
                    {
                        if (responseCallback != null)
                        {
                            responseCallback(httpRequestMessage, new RequestManagerResponse()
                            {
                                WebResponseStatus = WebResponseStatus.Success,
                                Text = null,
                                Stream = await response.Content.ReadAsInputStreamAsync()
                            });
                        }
                    }
                    else if (_responseType == ResponseType.TEXT)
                    {
                        if (responseCallback != null)
                        {
                            responseCallback(httpRequestMessage, new RequestManagerResponse()
                            {
                                WebResponseStatus = WebResponseStatus.Success,
                                Text = await response.Content.ReadAsStringAsync(),
                                Stream = null
                            });
                        }
                    }
                }
                else
                {
                    responseCallback(WebResponseStatus.ServerError, new RequestManagerResponse()
                    {
                        WebResponseStatus = WebResponseStatus.ServerError,
                        Text = null,
                        Stream = null
                    });
                }
            }
            catch (Exception e)
            {
                responseCallback(null, new RequestManagerResponse()
                {
                    WebResponseStatus = WebResponseStatus.ServerError,
                    Text = null,
                    Stream = null,
                    Error = new ErrorData() { ErrorMessage = e.Message }
                });
            }
        }
    }

    public class RequestManagerResponse
    {
        public string Text { get; set; }

        public Windows.Storage.Streams.IInputStream Stream { get; set; }

        public WebResponseStatus WebResponseStatus { get; set; }

        private ErrorData _errorData = new ErrorData();
        public ErrorData Error
        {
            get { return _errorData; }
            set { _errorData = value; }
        }
    }
}