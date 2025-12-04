using DLCGGameCore.Application.DTOs;
using DLCGGameCore.Application.Interfaces;
using DLCGGameCore.Domain.Entities;
using DLCGGameCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Infrastructure.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {

        private readonly AppDbContext _context;

        public VideoGameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideoGame>> GetAllAsync()
        => await _context.VideoGames.ToListAsync();

        public async Task<VideoGame?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _context.VideoGames.AsNoTracking().FirstOrDefaultAsync(vg => vg.Id == id, ct);

        public async Task<VideoGame> AddAsync(VideoGame game, CancellationToken ct = default)
        {
            _context.VideoGames.Add(game);
            await _context.SaveChangesAsync(ct);
            return game;
        }

        public async Task UpdateAsync(VideoGame game, CancellationToken ct = default)
        {
            _context.VideoGames.Update(game);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<PagedResult<VideoGame>> GetPagedAsync(int page, int pageSize, string? search, string? genre, string? sortBy, bool sortDesc, CancellationToken ct = default)
        {
            var query = _context.VideoGames.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(g => EF.Functions.Like(g.Title, $"%{s}%") || EF.Functions.Like(g.Genre, $"%{s}%"));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                var gfilter = genre.Trim();
                query = query.Where(g => g.Genre == gfilter);
            }

            // total count before paging
            var total = await query.CountAsync(ct);

            // sort
            query = (sortBy?.ToLower()) switch
            {
                "title" => sortDesc ? query.OrderByDescending(g => g.Title) : query.OrderBy(g => g.Title),
                "releaseyear" => sortDesc ? query.OrderByDescending(g => g.ReleaseYear) : query.OrderBy(g => g.ReleaseYear),
                "genre" => sortDesc ? query.OrderByDescending(g => g.Genre) : query.OrderBy(g => g.Genre),
                _ => query.OrderBy(g => g.Id)
            };

            var skip = (page - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync(ct);

            return new PagedResult<VideoGame>
            {
                Items = items,
                TotalItemCount = total,
                CurretnPage = page,
                PageSize = pageSize
            };
        }
    }
}
