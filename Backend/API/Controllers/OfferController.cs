using Aplication.DTOs;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/offers")]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerservice;

    public OfferController(IOfferService offerService)
    {
        _offerservice = offerService;
    }

    /* Actualiza el estado accepted de la oferta */
    [HttpPatch("{id:int}/state")]
    public async Task<ActionResult> UpdateOfferStateAsync(int id, [FromBody] bool isAccepted)
    {
        var offer = await _offerservice.UpdateOfferStateAsync(id, isAccepted);
        return offer is null ? NotFound($"La oferta con id {id} no existe.") : Ok(offer);
    }

    /* Obtiene todas las ofertas de un determinado post */
    [HttpGet("posts/{id:int}/offers")]
    public async Task<ActionResult> GetOfferByPostId(int id)
    {
        var offers = await _offerservice.GetOffersByPostIdAsync(id);
        return offers is null ? NotFound($"Las ofertas del post con id {id} no existen.") : Ok(offers);
    }

    /* Obtiene la cantidad de ofertas aceptadas por un usuario */
    [HttpGet("users/{userId:int}/offers/accepted/count")]
    public async Task<ActionResult> GetQuantityOffersAcceptedByUser(int userId)
    {
        var offers = await _offerservice.QuantityOffersAcceptedByUserAsync(userId);
        return Ok(offers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OfferRequestDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var offer = await _offerservice.CreateOfferAsync(dto);

            if (offer is null) return BadRequest("No se pudo crear la oferta correctamente");

            return Ok(offer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    /* Ofertas aceptadas por usuario classificado por fecha */
    [HttpGet("users/{id:int}/offers/accepted")]
    public async Task<ActionResult> GetOffersAcceptedByUser(int id)
    {
        var offer = await _offerservice.GetOffersAcceptedByUserAsync(id);
        return offer is null ? NotFound($"No se encontraron ofertas aceptadas por el usuario con id {id}") : Ok(offer);
    }
}
