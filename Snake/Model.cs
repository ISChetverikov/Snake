using SnakeOnlineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Model
    {
        const string _url = "http://safeboard.northeurope.cloudapp.azure.com";
        string _name = "Беспечный Едок";
        const string _token = "r_-0wJ$8PqCo.=3mH{Ap";
        Client _client;

        public string Name
        {
            get {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Model()
        {
            _client = new Client(_url);
        }

        public async Task GetNameAsync(string token)
        {
            await _client.GetNameAsync(_token).ContinueWith((antecedent) =>
            {
                Name = antecedent.Result.Name;
            });
        }
        

        public async Task<GameStateResponse> GetGameStateResponseAsync()
        {
            return await _client.GetGameStateResponseAsync();
        }
        
        public async Task SendDirectionRequestAsync(string direction)
        {
            await _client.SendDirectionRequestAsync(direction, _token);
        }
    }
}
