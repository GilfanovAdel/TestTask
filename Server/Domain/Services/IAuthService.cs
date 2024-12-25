using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(string username, string password, Guid organizationId);
        Task<Users?> LoginAsync(string username, string password);
    }
}
