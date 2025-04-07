using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Aplication.Interfaces;
using Aplication.DTOs;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ratings")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /* Obtiene todos los ratings de un usuario */
        [HttpGet("users/{id:int}")]
        public async Task<ActionResult> GetByRatedUser(int id)
        {
            var ratings = await _ratingService.GetByRatedUserAsync(id);
            return Ok(ratings);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RatingRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var rating = await _ratingService.CreateAsync(dto);

                if (rating is null) return BadRequest("No se pudo crear el rating correctamente");

                return CreatedAtAction(nameof(GetRatingById), new { id = rating.Data!.Id }, rating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /* Extras ----------------------------------------- */

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetRatingById(int id)
        {
            var rating = await _ratingService.GetByIdAsync(id);
            return rating is null ? NotFound($"El rating con id {id} no existe.") : Ok(rating);
        }

    }
}