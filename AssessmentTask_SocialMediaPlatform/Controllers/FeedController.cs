using Microsoft.AspNetCore.Mvc;
using Social_Media.Services;

namespace Social_Media.Controllers;

[ApiController]
[Route("feed")]
public class FeedController : ControllerBase
{
    private readonly FeedService _service;

    public FeedController(FeedService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetFeed()
    {
        return Ok(await _service.GetFeedAsync());
    }
}
