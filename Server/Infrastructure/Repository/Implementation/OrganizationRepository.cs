using Core.Entity;
using Core.Repository.Abstractions;
using Core.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Implementation
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(AppDbContext context) : base(context) { }
        public async Task<Organization> GetByNameAsync(string name)
        {
            var result = await _dbSet.Include(o => o.Users).FirstOrDefaultAsync(o => o.Name == name);
            return result;
        }
    }
}
