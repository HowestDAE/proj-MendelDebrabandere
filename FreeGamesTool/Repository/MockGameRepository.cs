using FreeGamesTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Net;

namespace FreeGamesTool.Repository
{
    public class MockGameRepository
    {
        private readonly HttpClient _httpClient;

        public MockGameRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Game> GetGameById(int gameId)
        {
            //Simulate server delay of 1 second
            await Task.Delay(1000);

            string json = File.ReadAllText("Resources/FallGuys.json");
            Game game = JsonConvert.DeserializeObject<Game>(json);

            return game;
        }

        public async Task<List<Game>> GetAllGames()
        {
            string json = File.ReadAllText("Resources/games.json");
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(json);

            return games;
        }

        public async Task<List<Game>> GetGamesByPlatformAndCategory(string platform, string category)
        {
            string json = File.ReadAllText("Resources/games.json");
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(json);

            return games;
        }
    }
}
