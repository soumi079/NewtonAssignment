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
        public async Task<IActionResult> GetPaged([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 5, CancellationToken ct = default)
        {
            currentPage = Math.Max(1, currentPage);
            pageSize = Math.Clamp(pageSize, 1, 200);
            var result = await _service.GetPagedAsync(currentPage, pageSize, ct);
                       
            var dtoResult = new PagedResult<VideoGameDto>
            {
                Items = result.Items,
                TotalItemCount = result.TotalItemCount,
                CurrentPage = result.CurrentPage,
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
