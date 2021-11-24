using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using LeaveWizard.WeatherForecast.Api.Specs.Core;

namespace LeaveWizard.WeatherForecast.Api.Specs.Context
{
    public class WebApiContext
    {
        public readonly string BaseUrl = "http://localhost/api/v1/";

        public IList<HttpResponseMessage> ApiResponses { get; set; } = new List<HttpResponseMessage>();
        public IList<HttpRequestMessage> ApiRequests { get; set; } = new List<HttpRequestMessage>();
    }
}