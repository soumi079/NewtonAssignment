using DLCGGameCore.Application.DTOs;
using DLCGGameCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Application.Interfaces
{
    public interface IVideoGameService
    {
        Task<PagedResult<VideoGameDto>> GetPagedAsync(int page, int pageSize, string? search, string? genre, string? sortBy, bool sortDesc, CancellationToken ct = default);
        Task<List<VideoGameDto>> GetAllAsync();
        Task<VideoGameDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<VideoGameDto> AddAsync(VideoGameDto dto, CancellationToken ct = default);
        Task UpdateAsync(VideoGameDto dto, CancellationToken ct = default);

    }
}
