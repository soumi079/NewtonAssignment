using DLCGGameCore.Application.DTOs;
using DLCGGameCore.Application.Interfaces;
using DLCGGameCore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DLCGGameCore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameService _service;

        public VideoGameController(IVideoGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5,
                                              [FromQuery] string? search = null, [FromQuery] string? genre = null,
                                              [FromQuery] string? sortBy = "title", [FromQuery] bool sortDesc = false,
                                              CancellationToken ct = default)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 200);
            var result = await _service.GetPagedAsync(page, pageSize, search, genre, sortBy, sortDesc, ct);
                       
            var dtoResult = new PagedResult<VideoGameDto>
            {
                Items = result.Items,
                TotalItemCount = result.TotalItemCount,
                CurretnPage = result.CurretnPage,
                PageSize = result.PageSize                
            };

            return Ok(dtoResult);
        }   

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var videoGames = await _service.GetByIdAsync(id);
            return Ok(videoGames);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VideoGameDto dto)
        {      
            var createdGame = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = createdGame.Id }, createdGame);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, VideoGameDto game)
        {
            if (id != game.Id) return BadRequest();
           
            await _service.UpdateAsync(game);
            return NoContent();
        }
    }
}
