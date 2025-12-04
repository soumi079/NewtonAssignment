using AutoMapper;
using DLCGGameCore.Application.DTOs;
using DLCGGameCore.Application.Interfaces;
using DLCGGameCore.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Application.Services
{
    public class VideoGameService: IVideoGameService
    {
        private readonly IVideoGameRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private const string GamesCacheKey = "games_list";

        public VideoGameService(IVideoGameRepository repository, IMemoryCache cache, IMapper mapper)
        {
            _repository = repository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<PagedResult<VideoGameDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            if (page == 1 && pageSize <= 50)
            {
                
                if (_cache.TryGetValue<PagedResult<VideoGameDto>>(GamesCacheKey, out var cached))
                    return cached;

                var domainPaged = await _repository.GetPagedAsync(page, pageSize, ct);
                var dtoPaged = new PagedResult<VideoGameDto>
                {
                    CurrentPage = domainPaged.CurrentPage,
                    PageSize = domainPaged.PageSize,
                    TotalItemCount = domainPaged.TotalItemCount,
                    Items = domainPaged.Items.Select(g => _mapper.Map<VideoGameDto>(g)).ToList()
                };

                _cache.Set(GamesCacheKey, dtoPaged, TimeSpan.FromMinutes(2));
                return dtoPaged;
            }

            var pageResult = await _repository.GetPagedAsync(page, pageSize, ct);
            return new PagedResult<VideoGameDto>
            {
                CurrentPage = pageResult.CurrentPage,
                PageSize = pageResult.PageSize,
                TotalItemCount = pageResult.TotalItemCount,
                Items = pageResult.Items.Select(g => _mapper.Map<VideoGameDto>(g)).ToList()
            };
        }

        public async Task<List<VideoGameDto>> GetAllAsync()
        {
            var videogames = await _repository.GetAllAsync();

            return videogames.Select(p => new VideoGameDto
            {
                Id = p.Id,
                Title = p.Title,
                Genre = p.Genre,
                ReleaseYear = p.ReleaseYear,
                Description = p.Description,
                Price = p.Price,
                Rating = p.Rating
            }).ToList();
        }

        public Task<VideoGameDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
           return _repository.GetByIdAsync(id, ct).ContinueWith(t => t.Result is null ? null : _mapper.Map<VideoGameDto>(t.Result), ct);
        }        

        public async Task<VideoGameDto> AddAsync(VideoGameDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<VideoGame>(dto);
            var created = await _repository.AddAsync(entity, ct);
            _cache.Remove(GamesCacheKey);
            return _mapper.Map<VideoGameDto>(created);
        }


        public async Task UpdateAsync(VideoGameDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<VideoGame>(dto);
            _cache.Remove(GamesCacheKey);
            await _repository.UpdateAsync(entity, ct);
        }        
    }
}
