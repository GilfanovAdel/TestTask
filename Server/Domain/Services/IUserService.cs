using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserService
    { 
        Task<List<Users>> GetUsersByOrganizationAsync(Guid organizationId);
    }
}
