using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Abstractions
{
    public interface IUserRepository : IRepository<Users>
    {
        public Task<List<Users>> GetAllByOrganizationIdAsync(string name);
        public Task<Users> GetbyRefreshTokenAsync(string token);

        public Task<Users> GetByLoginAsync(string login);
    }
}
