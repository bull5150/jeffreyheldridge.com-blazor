using CommonCore.Models;
using CommonCore.Models.BlueSky;
using RestAPI.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RestAPI.Services
{
    public class BlueSkyService: IBlueSkyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlueSkyService> _logger;
        private readonly IConfiguration _config;

        public BlueSkyService(HttpClient httpClient, ILogger<BlueSkyService> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
        }
        public async Task<string> GetProfileAsync(string handle)
        {
            try
            {
                // Example endpoint: Bluesky profile resolution
                var url = $"xrpc/com.atproto.identity.resolveHandle?handle={handle}";
                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Bluesky profile");
                throw;
            }
        }

        public async Task<string> GetSessionTokenAsync()
        {
            var username = _config["Bluesky:blueskyRobotID"];
            var password = _config["Bluesky:blueskyRobotPWD"];

            var payload = new
            {
                identifier = username,
                password = password
            };

            var response = await _httpClient.PostAsJsonAsync("xrpc/com.atproto.server.createSession", payload);
            response.EnsureSuccessStatusCode();
            var session = await response.Content.ReadFromJsonAsync<BlueSkySessionResponse>();
            return session?.AccessJwt ?? throw new Exception("No token returned");
        }

        public async Task<BlueSkyFeedResponse> GetAuthorFeedAsync(string actorHandle, int limit = 25)
        {
            try
            {
                var token = await GetSessionTokenAsync();
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"xrpc/app.bsky.feed.getAuthorFeed?actor={actorHandle}&limit={limit}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var feed = await response.Content.ReadFromJsonAsync<BlueSkyFeedResponse>();
                return feed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Bluesky author feed");
                throw;
            }
        }
    }
}
