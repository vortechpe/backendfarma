using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
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
        public IQueryable<Usuario> GetAllAsync(string filtro)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                // Aplicamos el filtro por nombre o correo
                query = query.Where(u => u.NombreUsuario.Contains(filtro));
            }

            return query;
        }

        public async Task<int> CountAsync(string filtro = null)
        {
            var query = _context.User.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(u => u.Nombre.Contains(filtro) || u.Email.Contains(filtro) || u.UserName.Contains(filtro));
            }

            return await query.CountAsync();  // Retornamos el total de usuarios que coinciden con el filtro
        }



        public async Task AddAsync(User user)
        {
            try
            {
                await _context.Set<User>().AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (CustomException e)
            {
                throw new CustomException(e.Message);
                throw;
            }
            
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
