using CommonCore.Models.BlueSky;

namespace RestAPI.Interfaces
{
    public interface IBlueSkyService
    {
        Task<string> GetProfileAsync(string handle);
        Task<BlueSkyFeedResponse> GetAuthorFeedAsync(string actorHandle, int limit = 25);
    }
}
