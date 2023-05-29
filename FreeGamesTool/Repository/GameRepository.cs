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
using System.Net;

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

            //Add 1 second delay
            await Task.Delay(1000);
            //DONT REMOVE THIS ISNT JUST FOR TESTING
            //THE API BLACKLISTED ME FOR AN HOUR BECAUSE I WAS DOING TOO MUCH REQUESTS WHILST TESTING

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                return null;
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            Game game = JsonConvert.DeserializeObject<Game>(jsonResponse);

            return game;
        }

        public async Task<List<Game>> GetAllGames()
        {
            string apiUrl = "https://www.freetogame.com/api/games";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(jsonResponse);

            return games;
        }

        public async Task<List<Game>> GetGamesByPlatformAndCategory(string platform, string category)
        {
            string apiUrl = $"https://www.freetogame.com/api/games?platform={platform}&category={category}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(jsonResponse);
            return games;
        }
    }
}
