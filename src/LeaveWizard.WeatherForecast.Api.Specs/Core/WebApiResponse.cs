﻿using System.Net;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    public class WebApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }

        public void ShouldStatusBe(HttpStatusCode expectedStatusCode, string message = null)
        {
            if (StatusCode != expectedStatusCode)
            {
                if (message != null)
                    message = ", because " + message;
                throw new HttpResponseException(StatusCode, ResponseMessage,
                    $"The Web API request expected to respond with {expectedStatusCode} ({(int) expectedStatusCode}), but responded with {StatusCode} ({(int) StatusCode}): '{ResponseMessage}'{message}."
                );
            }
        }
    }
    
    public class WebApiResponse<TData> : WebApiResponse
    {
        public TData ResponseData { get; set; }
    }
}