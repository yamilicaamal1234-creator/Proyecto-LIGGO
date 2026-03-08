using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Player player)
        {
            player.CreatedAt = DateTime.UtcNow;
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var player = await _context.Players
                .FirstOrDefaultAsync(p => p.Id == id && p.AdminId == adminId);

            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Player>> GetAllByAdminIdAsync(Guid adminId)
        {
            return await _context.Players
                .Where(p => p.AdminId == adminId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.Players
                .FirstOrDefaultAsync(p => p.Id == id && p.AdminId == adminId);
        }

        public async Task UpdateAsync(Player player)
        {
            var existingPlayer = await _context.Players
                .FirstOrDefaultAsync(p => p.Id == player.Id && p.AdminId == player.AdminId);

            if (existingPlayer != null)
            {
                existingPlayer.UpdatedAt = DateTime.UtcNow;
                existingPlayer.FullName = player.FullName;
                existingPlayer.DateOfBirth = player.DateOfBirth;
                existingPlayer.AssignedTeam = player.AssignedTeam;
                existingPlayer.GuardianName = player.GuardianName;
                existingPlayer.GuardianPhone = player.GuardianPhone;
                existingPlayer.Relationship = player.Relationship;
                
                _context.Players.Update(existingPlayer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
