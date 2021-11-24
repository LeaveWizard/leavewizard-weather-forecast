using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LeaveWizard.WeatherForecast.Api.Specs.Context;
using Newtonsoft.Json;
using LeaveWizard.WeatherForecast.Api.Specs.Core;

namespace LeaveWizard.WeatherForecast.Api.Specs.Drivers
{
    public class WebApiDriver : IDisposable
    {
        private readonly AppHostingContext _appHostingContext;
        private readonly WebApiContext _webApiContext;
        private readonly UserContext _userContext;
        private readonly StringBuilder _log = new();

        private HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                    _httpClient = _appHostingContext.CreateClient();
                return _httpClient;
            }
        }
        
        public WebApiDriver(AppHostingContext appHostingContext, 
                            WebApiContext webApiContext, 
                            UserContext userContext)
        {
            _appHostingContext = appHostingContext;
            _webApiContext = webApiContext;
            _userContext = userContext;
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        public WebApiResponse<TData> ExecuteGet<TData>(string endpoint)
        {
            return ExecuteSend<TData>(endpoint, null, HttpMethod.Get);
        }
        
        public WebApiResponse<TData> ExecutePost<TData>(string endpoint, object data)
        {
            return ExecuteSend<TData>(endpoint, data, HttpMethod.Post);
        }
        
        public WebApiResponse ExecuteDelete(string endpoint, object data = null)
        {
            return ExecuteSend<string>(endpoint,data, HttpMethod.Delete);
        }
        
        public WebApiResponse<TData> ExecuteDelete<TData>(string endpoint, object data = null)
        {
            return ExecuteSend<TData>(endpoint,data, HttpMethod.Delete);
        }

        public WebApiResponse ExecutePost(string endpoint, object data)
        {
            return ExecuteSend<string>(endpoint, data, HttpMethod.Post);
        }

        public WebApiResponse ExecutePut(string endpoint, object data)
        {
            return ExecuteSend<string>(endpoint, data, HttpMethod.Put);
        }

        private WebApiResponse<TData> ExecuteSend<TData>(string endpoint, object data, HttpMethod httpMethod)
        {
            // execute request
            var requestContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(httpMethod, _webApiContext.BaseUrl + endpoint)
            {
                Content = requestContent
            };
            
            if (!string.IsNullOrWhiteSpace(_userContext.AuthToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _userContext.AuthToken);
            }
            
            LogRequest(request);
            var response = HttpClient.SendAsync(request).Result;
            LogResponse(response);

            // for post requests the 2xx, 3xx and 4xx status codes are all "valid" results
            SanityCheck(response, 500);

            Console.WriteLine(GetResponseMessage(response));

            var responseContent = ReadContent(response);
            var responseData = default(TData);

            if ((int) response.StatusCode >= 200 && (int) response.StatusCode < 300)
            {
                responseData = typeof(TData) == typeof(string)
                    ? (TData) (object) responseContent
                    : JsonConvert.DeserializeObject<TData>(responseContent);
            }

            return new WebApiResponse<TData>
            {
                StatusCode = response.StatusCode,
                ResponseMessage = GetResponseMessage(response),
                ResponseData = responseData
            };
        }

        private string ReadContent(HttpResponseMessage response)
        {
            try
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                return null;
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void SanityCheck(HttpResponseMessage response, int upperRange = 300)
        {
            if ((int) response.StatusCode < 200 || (int) response.StatusCode >= upperRange)
            {
                var responseMessage = GetResponseMessage(response);
                throw new HttpResponseException(response.StatusCode, responseMessage,
                    $"The Web API request should be completed with success, not with error '{responseMessage}'");
            }
        }

        private void LogRequest(HttpRequestMessage request)
        {
            _webApiContext.ApiRequests.Add(request);
        }

        private string GetResponseMessage(HttpResponseMessage response)
        {
            if (response == null)
                return null;

            var content = ReadContent(response);
            return $"{response.StatusCode}: {content ?? response.ReasonPhrase}";
        }

        private void LogResponse(HttpResponseMessage response, string content = null)
        {
            _webApiContext.ApiResponses.Add(response);
            
            _log.AppendLine(response?.RequestMessage?.RequestUri?.ToString());
            _log.AppendLine($"{response?.StatusCode}: {response?.ReasonPhrase}");
            content ??= ReadContent(response);
            if (content != null)
                _log.AppendLine(content);
            _log.AppendLine();
        }

        public void SaveLog(string outputFolder, string fileName)
        {
            var logFilePath = Path.Combine(outputFolder, fileName);
            Console.WriteLine($"Saving log to {logFilePath}");
            File.WriteAllText(logFilePath, _log.ToString());
        }
    }
}