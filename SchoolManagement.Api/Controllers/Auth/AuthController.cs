using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
      
        private readonly AuthService _authService;
        private readonly IPermissionRepository _permRepo;
        public AuthController(AuthService authService, IPermissionRepository permRepo)
        {
            _authService = authService; _permRepo = permRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request.Username, request.Password, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");
            if (token == null )
                return Unauthorized(new { message = "Invalid credentials" });
            token.user = new
            {
                id = "0",
                role = "admin",
                displayName = "Abbott Keitch",
                photoURL = "/assets/images/avatars/brian-hughes.jpg",
                email = "admin@fusetheme.com",
                settings = new
                {
                    layout = new { },
                    theme = new { }
                },
                shortcuts = new[]
               {
                    "apps.calendar",
                    "apps.mailbox",
                    "apps.contacts"
                }
            };
            return Ok(token);
        }
        [HttpGet("access-token")]
        public async Task<IActionResult> AccessToken()
        {
            var dummyUser = new
            {
                id = "0",
                role = "admin",
                displayName = "Abbott Keitch",
                photoURL = "/assets/images/avatars/brian-hughes.jpg",
                email = "admin@fusetheme.com",
                settings = new
                {
                    layout = new { },
                    theme = new { }
                },
                shortcuts = new[]
               {
                    "apps.calendar",
                    "apps.mailbox",
                    "apps.contacts"
                }
            };
            return Ok(dummyUser);
        }

     
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest token)
        {
            var refreshToken = await _authService.GetByTokenAsync(token.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive)
                return Unauthorized("Invalid refresh token");
            var roles = refreshToken.User.UserRoles.Select(ur => ur.Role.Name).ToList();
            var permissions = await _permRepo.GetPermissionsByUserIdAsync(refreshToken.UserId);
            // Generate new tokens
            var newJwtToken = _authService.GenerateJwtToken(refreshToken.User, roles,permissions);
            var newRefreshToken = _authService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");

            // Mark old token as replaced
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            await _authService.UpdateAsync(refreshToken);
            newRefreshToken.UserId = refreshToken.UserId;
            await _authService.AddAsync(newRefreshToken);

            return Ok(new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token
            });
        }


    }
}
