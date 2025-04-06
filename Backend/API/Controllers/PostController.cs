using Aplication.DTOs.Posts;
using Aplication.Interfaces.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    
    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost([FromBody] PostRequestDTO postRequest)
    {
        try
        {
            var post = await _postService.CreateAsync(postRequest);
            return Ok(post);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error interno del servidor: {e.Message}");
        }
    }
    
    [HttpGet("near/get")]
    public async Task<IActionResult> GetNear([FromQuery] int currentLocationId)
    {
        var posts = await _postService.GetAllNearAsync(currentLocationId);
        return Ok(posts);
    }
    
    [HttpGet("near/{idLocation}")]
    public async Task<IActionResult> GetPostByLocation([FromRoute] int idLocation)
    {
        Console.WriteLine($"Recibido idLocation: {idLocation}");
        var posts = await _postService.GetPostByLocation(idLocation);
        return Ok(posts);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostByUser([FromQuery] int idUser)
    {
        var posts = await _postService.GetPostsByPosterUserIdAsync(idUser);
        return Ok(posts);
    }
    
    [HttpGet("count/{id}")]
    public async Task<IActionResult> GetPostCount([FromQuery] int idUser)
    {
        var posts = await _postService.GetPostsCountAsync(idUser);
        return Ok(posts);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        return Ok(post);
    }
    
    [HttpGet("active")]
    public async Task<IActionResult> GetActivePosts()
    {
        var post = await _postService.GetActivePosts();
        return Ok(post);
    }

    [HttpPatch("{id:int}/accepted")]
    public async Task<IActionResult> MarkAsAccepted(int id)
    {
        try
        {
            var post = await _postService.MarkAsAcceptedAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error interno del servidor: {e.Message}");
        }
    }
}