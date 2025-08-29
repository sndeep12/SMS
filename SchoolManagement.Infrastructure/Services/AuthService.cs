using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Infrastructure.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Infrastructure.Services
{
    public class AuthService : IAuthService, IRefreshTokenRepository
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permRepo;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IConfiguration config, IPermissionRepository permRepo)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _config = config;
            _permRepo = permRepo;
        }
        public string GenerateJwtToken(User user, List<string> roles, List<string> permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            foreach (var role in roles.Distinct())
                claims.Add(new Claim(ClaimTypes.Role, role));

            foreach (var p in permissions.Distinct())
                claims.Add(new Claim("Permission", p));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:AccessTokenExpirationMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       
        public async Task<LoginResponse?> AuthenticateAsync(string username, string password, string ipAddress)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return null;

            if (!VerifyPassword(password, user.PasswordHash))
                return null;
          
        
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
            var permissions = await _permRepo.GetPermissionsByUserIdAsync(user.UserId);

            var newJwtToken = GenerateJwtToken(user, roles, permissions);
            var newRefreshToken = GenerateRefreshToken();
            var refreshToken=GenerateRefreshToken(ipAddress);
            refreshToken.UserId = user.UserId;
            await AddAsync(refreshToken);
            return new LoginResponse
            {
                token = newJwtToken,
                refreshToken = newRefreshToken,                
            };
        }
        
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(_config["Jwt:RefreshTokenExpirationDays"])),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
        private bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash == storedHash;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _refreshTokenRepository.AddAsync(token);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _refreshTokenRepository.GetByTokenAsync(token);
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            await _refreshTokenRepository.UpdateAsync(token);

        }

       
    }
}
