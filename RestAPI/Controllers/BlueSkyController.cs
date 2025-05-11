using Microsoft.AspNetCore.Mvc;
using RestAPI.Interfaces;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlueSkyController : ControllerBase
    {
        private readonly IBlueSkyService _blueskyService;

        public BlueSkyController(IBlueSkyService blueskyService)
        {
            _blueskyService = blueskyService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile([FromQuery] string handle)
        {
            var result = await _blueskyService.GetProfileAsync(handle);
            return Ok(result);
        }
        [HttpGet("feed")]
        public async Task<IActionResult> GetAuthorFeed([FromQuery] string handle, [FromQuery] int limit = 25)
        {
            var result = await _blueskyService.GetAuthorFeedAsync(handle, limit);
            return Ok(result);
        }
    }
}
