using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamGameViewerApp.Models;
using SteamGameViewerApp.Services;
using System.Drawing;

namespace SteamGameViewerApp
{
    public partial class Form1 : Form
    {
        private readonly SteamService _steamService;
        private string currentSteamId64;

        public Form1()
        {
            InitializeComponent();

            const string apiKey = "1194738B312415CB40558DB2E8F0375F";
            _steamService = new SteamService(apiKey);

            profileLink.Click += LinkLabel2_Click;
            lstFriends.DoubleClick += LstFriends_DoubleClick;
            lstPlayedGames.DoubleClick += LstPlayedGames_DoubleClick;
            lstPileOfShame.DoubleClick += PileofShames_DoubleClick;
            profileInfo.Click += BtnFetch_Click;
            btnClear.Click += BtnClear_Click;
        }

        private async void BtnFetch_Click(object sender, EventArgs e)
        {
            await LoadProfileDataAsync(txtVanityUrl.Text.Trim());
        }

        private async Task LoadProfileDataAsync(string vanityOrSteamId)
        {
            lstPlayedGames.Items.Clear();
            lstPileOfShame.Items.Clear();
            lstFriends.Items.Clear();
            totalCost.Text = "Total Cost: RM 0.00";
            ClearProfileInfo();

            if (string.IsNullOrEmpty(vanityOrSteamId))
            {
                MessageBox.Show("Please enter a vanity URL username or SteamID.");
                return;
            }

            string steamId = vanityOrSteamId;
            if (!ulong.TryParse(vanityOrSteamId, out _))
            {
                steamId = await _steamService.ResolveVanityURLAsync(vanityOrSteamId);
                if (steamId == null)
                {
                    MessageBox.Show("Could not resolve vanity URL.");
                    return;
                }
            }

            currentSteamId64 = steamId;

            var profile = await _steamService.GetPlayerSummaryAsync(steamId);
            if (profile == null)
            {
                MessageBox.Show("Could not retrieve profile info. Profile may be private.");
                return;
            }
            DisplayProfileInfo(profile.Value);

            var games = await _steamService.GetOwnedGamesAsync(steamId);
            if (games == null)
            {
                MessageBox.Show("Could not retrieve games. Profile may be private.");
                return;
            }

            double totalMYR = 0;

            foreach (var gameJson in games)
            {
                int playtime = gameJson.GetProperty("playtime_forever").GetInt32();
                string name = gameJson.GetProperty("name").GetString();
                int appid = gameJson.GetProperty("appid").GetInt32();

                double price = await _steamService.GetGamePriceMYRAsync(appid);
                totalMYR += price;

                var gameItem = new SteamGame
                {
                    Name = name,
                    AppId = appid,
                    Hours = playtime / 60.0
                };

                if (playtime == 0)
                    lstPileOfShame.Items.Add(gameItem);
                else
                    lstPlayedGames.Items.Add(gameItem);
            }

            totalCost.Text = $"Total Cost: RM {totalMYR:F2}";
            totalGames.Text = $"Total Games: {games.Length}";

            var friendIds = await _steamService.GetFriendsListAsync(steamId);
            if (friendIds != null && friendIds.Count > 0)
            {
                var friendNames = await _steamService.GetPlayersSummariesAsync(friendIds);

                foreach (var friendId in friendIds)
                {
                    if (friendNames.TryGetValue(friendId, out string name))
                        lstFriends.Items.Add($"{name} ({friendId})");
                    else
                        lstFriends.Items.Add(friendId);
                }
            }
            else
            {
                lstFriends.Items.Add("No friends found or profile is private.");
            }
        }

        private void ClearProfileInfo()
        {
            steamid.Text = "SteamID:";
            steamid3.Text = "SteamID3:";
            steamid64dec.Text = "SteamID64 (Dec):";
            steamidhex.Text = "SteamID64 (Hex):";
            profiles.Text = "Profiles:";
            profileCreated.Text = "Profile Created:";
            profilePicture.Image = null;
            profileLink.Text = "profiles";
            profileLink.Tag = null;
        }

        private void DisplayProfileInfo(JsonElement profile)
        {
            string steamId64 = profile.GetProperty("steamid").GetString();
            steamid64dec.Text = $"SteamID64 (Dec): {steamId64}";

            var (accountId, steamId64Str, steamId32, steamId3, steamIdHex) = _steamService.GetSteamIdFormats(steamId64);

            steamid32.Text = $"SteamID32: {accountId}";
            steamid3.Text = $"SteamID3: {steamId3}";
            steamidhex.Text = $"SteamID64 (Hex): {steamIdHex}";
            steamid.Text = $"SteamID: {steamId64Str}";

            string profileUrl = profile.GetProperty("profileurl").GetString();
            profileLink.Text = profileUrl;
            profileLink.Tag = profileUrl;

            if (profile.TryGetProperty("timecreated", out JsonElement timeCreatedElem))
            {
                long timeCreatedUnix = timeCreatedElem.GetInt64();
                var dtOffset = DateTimeOffset.FromUnixTimeSeconds(timeCreatedUnix);
                profileCreated.Text = $"Profile Created: {dtOffset.LocalDateTime:yyyy-MM-dd HH:mm:ss}";
            }
            else
            {
                profileCreated.Text = "Profile Created: N/A";
            }

            if (profile.TryGetProperty("avatarfull", out JsonElement avatarElem))
            {
                string avatarUrl = avatarElem.GetString();
                try
                {
                    var imageStream = _steamService._httpClient.GetStreamAsync(avatarUrl);
                    imageStream.ContinueWith(task =>
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            var img = Image.FromStream(task.Result);
                            profilePicture.Invoke(new Action(() => profilePicture.Image = img));
                        }
                    });
                }
                catch { }
            }
        }

        private void LstPlayedGames_DoubleClick(object sender, EventArgs e)
        {
            if (lstPlayedGames.SelectedItem is SteamGame selectedGame)
            {
                OpenSteamStorePage(selectedGame.AppId);
            }
        }

        private void PileofShames_DoubleClick(object sender, EventArgs e)
        {
            if (lstPileOfShame.SelectedItem is SteamGame selectedGame)
            {
                OpenSteamStorePage(selectedGame.AppId);
            }
        }

        private void OpenSteamStorePage(int appId)
        {
            string url = $"https://store.steampowered.com/app/{appId}/";
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Unable to open store page.");
            }
        }

        private async void LstFriends_DoubleClick(object sender, EventArgs e)
        {
            if (lstFriends.SelectedItem is string selected)
            {
                var startIdx = selected.LastIndexOf('(');
                var endIdx = selected.LastIndexOf(')');
                if (startIdx != -1 && endIdx != -1 && endIdx > startIdx)
                {
                    string steamId = selected.Substring(startIdx + 1, endIdx - startIdx - 1);
                    await LoadProfileDataAsync(steamId);
                }
            }
        }

        private void LinkLabel2_Click(object sender, EventArgs e)
        {
            if (profileLink.Tag is string url)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Unable to open link.");
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lstPlayedGames.Items.Clear();
            lstPileOfShame.Items.Clear();
            lstFriends.Items.Clear();
            totalCost.Text = "Total Cost: RM 0.00";
            ClearProfileInfo();
            txtVanityUrl.Clear();
            currentSteamId64 = null;
        }

        private void lblFriendsList_Click(object sender, EventArgs e)
        {

        }
        private void lstPileOfShame_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void steamid64dec_Click(object sender, EventArgs e) { }

        private void steamidhex_Click(object sender, EventArgs e) { }

        private void profileCreated_Click(object sender, EventArgs e) { }

        private void profilePicture_Click(object sender, EventArgs e) { }

        private void steamid32_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
