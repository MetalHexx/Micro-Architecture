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

        protected Task<ApiResult<ResponseType>> GetAsync<ResponseType>(string path)
        {
            return HandleRequestAsync<ResponseType>(path, HttpMethod.Get);
        }

        protected async Task<ApiResult<ResponseType>> PostAsync<ResponseType>(string path, object content)
        {
            var sContent = JsonConvert.SerializeObject(content);
            return await HandleRequestAsync<ResponseType>(path, HttpMethod.Post, sContent);
        }

        protected async Task<ApiResult<ResponseType>> PutAsync<ResponseType>(string path, object content)
        {
            var sContent = JsonConvert.SerializeObject(content);
            return await HandleRequestAsync<ResponseType>(path, HttpMethod.Put, sContent);
        }

        protected Task<ApiResult<ResponseType>> DeleteAsync<ResponseType>(string path)
        {
            return HandleRequestAsync<ResponseType>(path, HttpMethod.Delete);
        }

        protected async Task<ApiResult<ResponseType>> HandleRequestAsync<ResponseType>(string path, HttpMethod method, string content = null)
        {
            try
            {
                using (_client)
                using (var response = await SendRequestAsync(path, method, content))
                {
                    var resContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == false)
                    {
                        return new ApiResult<ResponseType>
                        {
                            StatusCode = (int)response.StatusCode,
                            Content = resContent,
                            IsSuccess = false
                        };
                    }
                    return new ApiResult<ResponseType>
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = resContent,
                        ContentObject = JsonConvert.DeserializeObject<ResponseType>(resContent),
                        IsSuccess = false
                    };
                }

            }
            catch(Exception e)
            {
                var x = 0;
                throw;
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
