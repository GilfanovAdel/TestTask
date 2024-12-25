using Core.Dto;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Abstraction
{
    public interface IUserService
    { 
        Task<List<UserDto>> GetUsersByOrganizationAsync( string name);
    }
}
