using Microsoft.EntityFrameworkCore;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Contexts;
using StudyDesck.API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Persistence.Repositories
{
    public class PlatformRepository : BaseRepository, IPlatformRepository
    {
        public PlatformRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Platform platform)
        {
            await _context.Platforms.AddAsync(platform);
        }

        public async Task<Platform> FindById(int id)
        {
            return await _context.Platforms.FindAsync(id);
        }

        public async Task<IEnumerable<Platform>> ListAsync()
        {
            return await _context.Platforms.ToListAsync();
        }

        public void Remove(Platform platform)
        {
            _context.Platforms.Remove(platform);
        }

        public void Update(Platform platform)
        {
            _context.Platforms.Update(platform);
        }
    }
}
