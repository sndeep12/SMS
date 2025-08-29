using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities.Auth;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
         .Include(u => u.UserRoles)
             .ThenInclude(ur => ur.Role)
         .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
