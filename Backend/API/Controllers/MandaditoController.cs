using Aplication.DTOs.Mandaditos;
using Aplication.Interfaces.Mandaditos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/mandadito")]
public class MandaditoController : ControllerBase
{
    private readonly IMandaditoService _mandaditoService;
    
    public MandaditoController(IMandaditoService mandaditoService)
    {
        _mandaditoService = mandaditoService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetMandaditoById(int id)
    {
        var mandadito = await _mandaditoService.GetByIdAsync(id);
        return mandadito is null ? NotFound($"El mandadito con id {id} no existe.") : Ok(mandadito);
    }
    
    [HttpGet("deliveries/count/{idUser}")]
    public async Task<ActionResult> DeliveriesCount(int idUser)
    {
        var cant = await _mandaditoService.DeliveriesCount(idUser);
        return Ok(cant);
    }
    
    [HttpGet("history/get")]
    public async Task<IActionResult> GetHistory([FromQuery] int userId)
    {
        var mandaditos = await _mandaditoService.GetHistoryAsync(userId);
        return Ok(mandaditos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MandaditoRequestDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _mandaditoService.CreateAsync(dto);
        return Ok(response);
    }
    
    [HttpGet("runner")]
    public async Task<ActionResult> GetMandaditosActiveByRunner()
    {
        var mandadito = await _mandaditoService.GetActivesByRunnerId();
        return mandadito is null ? NotFound($"El mandadito no existe.") : Ok(mandadito);
    }
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<Dictionary<string, List<MandaditoDetailDto>>>>> GetHistoryMandaditos()
    {
        var history = await _mandaditoService.Execute();
    
        var result = history
            .Select(kvp => new Dictionary<string, List<MandaditoDetailDto>>
            {
                {
                    kvp.Key,
                    kvp.Value.Select(m => new MandaditoDetailDto
                    {
                        Id = m.Id,
                        Title = m.Post?.Title ?? "No title",
                        Description = m.Post?.Description ?? string.Empty,
                        AcceptedRate = m.AcceptedRate,
                        AcceptedAt = m.AcceptedAt,
                        DeliveredAt = m.DeliveredAt,
                        SecurityCode = m.SecurityCode,
                        PickupLocation = m.Post?.PickUpLocation != null ? new LocationDto
                        {
                            Id = m.Post.PickUpLocation.Id,
                            Name = m.Post.PickUpLocation.Name,
                        } : null,
                        DeliveryLocation = m.Post?.DeliveryLocation != null ? new LocationDto
                        {
                            Id = m.Post.DeliveryLocation.Id,
                            Name = m.Post.DeliveryLocation.Name,
                        } : null
                    }).ToList()
                }
            })
            .ToList();

        return Ok(result);
    }

    [HttpGet("history/runner")]
    public async Task<ActionResult<IEnumerable<Dictionary<string, List<MandaditoDetailDto>>>>> GetHistoryMandaditosAsRunner()
    {
        var history = await _mandaditoService.ExecuteGet();
    
        var result = history
            .Select(kvp => new Dictionary<string, List<MandaditoDetailDto>>
            {
                {
                    kvp.Key,
                    kvp.Value.Select(m => new MandaditoDetailDto
                    {
                        Id = m.Id,
                        Title = m.Post?.Title ?? "No title",
                        Description = m.Post?.Description ?? string.Empty,
                        AcceptedRate = m.AcceptedRate,
                        AcceptedAt = m.AcceptedAt,
                        DeliveredAt = m.DeliveredAt,
                        SecurityCode = m.SecurityCode,
                        PickupLocation = m.Post?.PickUpLocation != null ? new LocationDto
                        {
                            Id = m.Post.PickUpLocation.Id,
                            Name = m.Post.PickUpLocation.Name,
                        } : null,
                        DeliveryLocation = m.Post?.DeliveryLocation != null ? new LocationDto
                        {
                            Id = m.Post.DeliveryLocation.Id,
                            Name = m.Post.DeliveryLocation.Name,
                        } : null
                    }).ToList()
                }
            })
            .ToList();

        return Ok(result);
    }

    /* Obtien el id del mandadito debido a un post */
    [HttpGet("post/{idPost:int}")]
    public async Task<ActionResult> GetMandaditoByPostId(int idPost)
    {
        var mandadito = await _mandaditoService.GetByPostIdAsync(idPost);
        return mandadito is null ? NotFound($"El mandadito con id {idPost} no existe.") : Ok(mandadito);
    }
}