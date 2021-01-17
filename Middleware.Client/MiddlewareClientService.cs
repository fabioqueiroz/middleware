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
            var device = new Device
            {
                Payload = new byte[1234],
                Longitude = 66.8888,
                Latitude = 22.4444
            };

            //var response = await _httpClient.PostAsync("request",
            //    new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));
            //var response = _httpClient.PostAsJsonAsync("request",
            //    new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json")).Result;
            var response = _httpClient.PostAsJsonAsync("request", device);

            try
            {
                //response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
