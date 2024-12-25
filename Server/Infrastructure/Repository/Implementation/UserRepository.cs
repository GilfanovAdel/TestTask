using Core.Entity;
using Core.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Implementation
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Users>> GetAllByOrganizationIdAsync(string name)
        {
            return await _dbSet
        .Include(u => u.Organization)
        .Where(u => u.Organization.Name == name)
        .ToListAsync();
        }

        public async Task<Users> GetByLoginAsync(string login)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<Users> GetbyRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                                     .Include(rt => rt.User)
                                     .FirstOrDefaultAsync(rt => rt.Token == token);
            return refreshToken.User;
        }
    }
}
