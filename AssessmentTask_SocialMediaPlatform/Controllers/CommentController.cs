using Microsoft.AspNetCore.Mvc;
using Social_Media.Models;
using Social_Media.Services;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly CommentService _service;

    public CommentController(CommentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Comment comment)
    {
        try
        {
            return Ok(await _service.CreateAsync(comment));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
