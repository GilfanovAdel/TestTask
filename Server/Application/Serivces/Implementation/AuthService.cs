using Core.Dto;
using Core.Entity;
using Core.Repository.Abstractions;
using Core.Serivces.Abstraction;
using Core.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Serivces.Implementation
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private IPasswordHasher<object> _passwordHasher;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IPasswordHasher<object> passwordHasher, IRefreshTokenRepository refreshTokenRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByLoginAsync(loginDto.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Неверное имя пользователя или пароль.");
            }
            if (_passwordHasher.HashPassword(null, loginDto.Password) == user.PasswordHash)
            {
                throw new UnauthorizedAccessException("Неверное имя пользователя или пароль.");
            }
            var jwtToken = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, Token = refreshToken });
            return new LoginResponseDto() { JwtToken = jwtToken, RefreshToken = refreshToken };

        }

        public async Task<LoginResponseDto> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            var existingUser = await _userRepository.GetByLoginAsync(userRegistrationDto.Login);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Пользователь с таким именем уже существует.");
            }

          
            var hashedPassword = _passwordHasher.HashPassword(null,userRegistrationDto.Password);

            var user = new Users
            {
                Login = userRegistrationDto.Login,
                PasswordHash = hashedPassword,
                OrganizationId= userRegistrationDto.Organizationid,
            };

            await _userRepository.AddAsync(user);

            var jwtToken = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, Token = refreshToken });

            return new LoginResponseDto() { JwtToken=jwtToken, RefreshToken = refreshToken };   
        }
    }
}
