using DLCGGameCore.Domain.Entities;
using DLCGGameCore.Infrastructure.Data;
using DLCGGameCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace DLCGGameCore.Tests.Repository
{
    public class GameRepositoryTests
    {
        private AppDbContext CreateContext()
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(opts);
        }

        [Fact]
        public async Task GetPagedAsync_Returns_PagedResult()
        {
            var db = CreateContext();
            db.VideoGames.AddRange(new VideoGame { Title = "A", Genre = "X", ReleaseYear = 2000 },
                                   new VideoGame { Title = "B", Genre = "Y", ReleaseYear = 2001 });
            await db.SaveChangesAsync();

            var repo = new VideoGameRepository(db);
            var res = await repo.GetPagedAsync(1, 10);
            Assert.Equal(2, res.TotalItemCount);
            Assert.Equal(2, res.Items.Count);
        }
    }
}
