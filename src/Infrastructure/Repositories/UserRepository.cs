using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
        public IQueryable<User> GetAllAsync()
        {
            return _context.User.AsQueryable();  // Esto devuelve un IQueryable para poder aplicar Skip y Take
        }

        public async Task<int> CountAsync()
        {
            return await _context.User.CountAsync();  // Contar el total de usuarios
        }

       

        public async Task AddAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
