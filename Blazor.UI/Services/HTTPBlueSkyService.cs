using CommonCore.Models.BlueSky;
using System.Net.Http.Json;


namespace Blazor.UI.Services
{
    public class HTTPBlueSkyService
    {
        private readonly HttpClient _http;

        public HTTPBlueSkyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<BlueSkyFeedResponse> GetLatestPostsAsync()
        {
            return await _http.GetFromJsonAsync<BlueSkyFeedResponse>("/api/BlueSky/feed?handle=jheldridge.com&limit=25");
        }
    }
}
