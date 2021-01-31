using Middleware.Api;
using Middleware.Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Middleware.Api.Models;

namespace Middleware.Client
{
    public class MiddlewareClientService : IMiddlewareService
    {
        private HttpClient _httpClient = new HttpClient();
        public MiddlewareClientService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44307/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task Run()
        {
            //await GetMiddlewareRespose();
            await CreateRequest();
        }

        public async Task GetMiddlewareRespose()
        {
            var response = await _httpClient.GetAsync("request/json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
        }

        public async Task CreateRequest()
        {
            var device = new DeviceModel
            {
                Payload = new byte[4] { 15, 25, 35, 45 },
                Longitude = 66.8888,
                Latitude = 22.4444,
                DelegateName = "HostDelegate"
            };

            //var response = await _httpClient.PostAsync("request",
            //    new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));
            //var response = _httpClient.PostAsJsonAsync("request",
            //    new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json")).Result;

            var serializedDevice = JsonConvert.SerializeObject(device);
            var request = new HttpRequestMessage(HttpMethod.Post, "request");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedDevice);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
