using FreeGamesTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;

namespace FreeGamesTool.Repository
{
    public class GameRepository
    {
        private readonly HttpClient _httpClient;

        public GameRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Game> GetGameById(int gameId)
        {
            //Get the data from the website
            string apiUrl = $"https://www.freetogame.com/api/game?id={gameId}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            Game game = JsonConvert.DeserializeObject<Game>(jsonResponse);

            return game;
        }
    }
}
