using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SnakeOnlineClient
{
    public sealed class Client
    {
        private readonly RestClient _httpClient;

        public Client(string uri)
        {
            _httpClient = new RestClient(uri);
        }

        public string GetName(string token)
        {
            var request = new RestRequest("api/Player/name");
            request.AddQueryParameter("token", token);
            var response = _httpClient.Execute<NameResponse>(request);

            if (!response.IsSuccessful)
                return null;

            return response.Data.Name;
        }

        public async Task<string> GetNameAsync(string token)
        {
            var request = new RestRequest("api/Player/name");
            request.AddQueryParameter("token", token);
            var response = await _httpClient.ExecuteGetTaskAsync<NameResponse>(request);
            return response.Data.Name;
        }

        public async Task<GameStateResponse> GetGameStateResponseAsync()
        {
            var request = new RestRequest("api/Player/gameboard");
            var response = await _httpClient.ExecuteGetTaskAsync<GameStateResponse>(request);
            return response.Data;
        }

        public GameStateResponse GetGameStateResponse()
        {
            var request = new RestRequest("api/Player/gameboard");
            var response = _httpClient.Execute<GameStateResponse>(request);
            return response.Data;
        }

        public HttpStatusCode SendDirectionRequest(string direction, string token)
        {
            var request = new RestRequest("api/Player/direction")
            {
                Method = Method.POST
            };
            request.AddJsonBody(new DirectionRequest { Direction = direction, Token = token });
            var response = _httpClient.Execute(request);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> SendDirectionRequestAsync(string direction, string token)
        {
            var request = new RestRequest("api/Player/direction");
            request.Method = Method.POST;
            request.AddJsonBody(new DirectionRequest { Direction = direction, Token = token });
            var response = await _httpClient.ExecuteTaskAsync(request);
            
            return response.StatusCode;
        }
    }
}
