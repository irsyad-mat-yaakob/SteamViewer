using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SteamGameViewerApp.Services
{
    public class SteamService
    {
        public readonly HttpClient _httpClient;
        public readonly string _apiKey;

        private const ulong SteamIdOffset = 76561197960265728;

        public SteamService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }

        public async Task<string> ResolveVanityURLAsync(string vanity)
        {
            string url = $"https://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key={_apiKey}&vanityurl={vanity}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
            var root = json.RootElement.GetProperty("response");

            if (root.GetProperty("success").GetInt32() != 1)
                return null;

            return root.GetProperty("steamid").GetString();
        }

        public async Task<JsonElement[]> GetOwnedGamesAsync(string steamId)
        {
            string url = $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={_apiKey}&steamid={steamId}&include_appinfo=1";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
            var root = json.RootElement.GetProperty("response");

            if (!root.TryGetProperty("games", out JsonElement games))
                return null;

            return games.EnumerateArray().ToArray();
        }

        public async Task<double> GetGamePriceMYRAsync(int appId)
        {
            string url = $"https://store.steampowered.com/api/appdetails?appids={appId}&cc=my&l=en";

            for (int attempt = 0; attempt < 2; attempt++)
            {
                try
                {
                    var response = await _httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode) continue;

                    var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                    if (!json.RootElement.TryGetProperty(appId.ToString(), out JsonElement gameData))
                        continue;

                    if (gameData.GetProperty("success").GetBoolean())
                    {
                        var data = gameData.GetProperty("data");
                        if (data.TryGetProperty("price_overview", out JsonElement priceOverview))
                        {
                            int finalPriceCents = priceOverview.GetProperty("final").GetInt32();
                            return finalPriceCents / 100.0;
                        }
                    }
                }
                catch
                {
                    // ignore and retry
                }
            }

            return 0;
        }

        public async Task<List<string>> GetFriendsListAsync(string steamId)
        {
            string url = $"https://api.steampowered.com/ISteamUser/GetFriendList/v1/?key={_apiKey}&steamid={steamId}&relationship=friend";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                var root = json.RootElement.GetProperty("friendslist");

                if (!root.TryGetProperty("friends", out JsonElement friends))
                    return null;

                return friends.EnumerateArray().Select(f => f.GetProperty("steamid").GetString()).ToList();
            }
            catch { return null; }
        }

        public async Task<Dictionary<string, string>> GetPlayersSummariesAsync(List<string> steamIds)
        {
            if (steamIds == null || steamIds.Count == 0)
                return new Dictionary<string, string>();

            string url = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={_apiKey}&steamids={string.Join(",", steamIds)}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return new Dictionary<string, string>();

                var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                var players = json.RootElement.GetProperty("response").GetProperty("players");

                return players.EnumerateArray().ToDictionary(
                    p => p.GetProperty("steamid").GetString(),
                    p => p.GetProperty("personaname").GetString()
                );
            }
            catch { return new Dictionary<string, string>(); }
        }

        public async Task<JsonElement?> GetPlayerSummaryAsync(string steamId)
        {
            string url = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={_apiKey}&steamids={steamId}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                var players = json.RootElement.GetProperty("response").GetProperty("players");

                return players.GetArrayLength() > 0 ? players[0] : (JsonElement?)null;
            }
            catch { return null; }
        }

        public (ulong accountId, string steamId64, string steamId32, string steamId3, string steamIdHex) GetSteamIdFormats(string steamId64)
        {
            ulong steamId64Num = ulong.Parse(steamId64);
            ulong accountId = steamId64Num - SteamIdOffset;
            string steamId32 = accountId.ToString();
            string steamId3 = $"[U:1:{accountId}]";
            string steamIdHex = $"0x{steamId64Num:X}";

            return (accountId, steamId64, steamId32, steamId3, steamIdHex);
        }
    }
}