using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CbInsights.Clients
{
    public abstract class ClientBase
    {
        private readonly HttpClient _client;

        public ClientBase(HttpClient client, string baseAddress)
        {
            _client = client;
            _client.BaseAddress = new Uri(baseAddress);
        }

        protected Task<ApiResult<T>> GetAsync<T>(string path)
        {
            return HandleRequestAsync<T>(path, HttpMethod.Get);
        }

        protected async Task<ApiResult<T>> PostAsync<T>(string path, T content)
        {
            var sContent = JsonConvert.SerializeObject(content);
            return await HandleRequestAsync<T>(path, HttpMethod.Post, sContent);
        }

        protected async Task<ApiResult<T>> PutAsync<T>(string path, T content)
        {
            var sContent = JsonConvert.SerializeObject(content);
            return await HandleRequestAsync<T>(path, HttpMethod.Put, sContent);
        }

        protected Task<ApiResult<T>> Delete<T>(string path)
        {
            return HandleRequestAsync<T>(path, HttpMethod.Delete);
        }

        protected async Task<ApiResult<T>> HandleRequestAsync<T>(string path, HttpMethod method, string content = null)
        {
            using (_client)
            using (var response = await SendRequestAsync(path, method, content))
            {
                var resContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                {
                    return new ApiResult<T>
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = resContent,
                        IsSuccess = false
                    };
                }
                return new ApiResult<T>
                {
                    StatusCode = (int)response.StatusCode,
                    Content = resContent,
                    ContentObject = JsonConvert.DeserializeObject<T>(resContent),
                    IsSuccess = false
                };
            }
        }

        private async Task<HttpResponseMessage> SendRequestAsync(string path, HttpMethod method, string content = null)
        {
            ByteArrayContent byteContent = null;

            if (content != null)
            {
                var buffer = Encoding.UTF8.GetBytes(content);
                byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            switch (method.Method.ToLower())
            {
                case "get":
                    return await _client.GetAsync(path);
                case "post":
                    return await _client.PostAsync(path, byteContent);
                case "put":
                    return await _client.PutAsync(path, byteContent);
                case "delete":
                    return await _client.DeleteAsync(path);
                default:
                    throw new ArgumentException("Unsupported method");                
            }
        }
    }
}
