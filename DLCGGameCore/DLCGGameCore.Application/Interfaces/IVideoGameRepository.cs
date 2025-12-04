using DLCGGameCore.Application.DTOs;
using DLCGGameCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Application.Interfaces
{
    public interface IVideoGameRepository
    {
        Task<PagedResult<VideoGame>> GetPagedAsync(
        int page, int pageSize, CancellationToken ct);
        Task<List<VideoGame>> GetAllAsync();
        Task<VideoGame?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<VideoGame> AddAsync(VideoGame videoGame, CancellationToken ct = default);

        Task UpdateAsync(VideoGame videoGame, CancellationToken ct = default);
        
    }
}
