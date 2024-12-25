using Core.Dto;
using Core.Entity;
using Core.Repository.Abstractions;
using Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Serivces.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetUsersByOrganizationAsync(string name)
        {
           var result = await _userRepository.GetAllByOrganizationIdAsync(name);
            return result.Select(x => new UserDto() {Login = x.Login}).ToList();
        }
    }
}
