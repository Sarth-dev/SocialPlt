using Microsoft.AspNetCore.Mvc;
using Social_Media.Models;

[ApiController]
[Route("[controller]")]
public class LikeController : ControllerBase
{
    private readonly LikeService _service;

    public LikeController(LikeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Like(Like like)
    {
        try
        {
            await _service.LikeAsync(like);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
