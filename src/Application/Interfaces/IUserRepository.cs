using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByEmailAsync(string email);
        IQueryable<User> GetAllAsync();

        // Método para contar el número total de usuarios
        Task<int> CountAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
    }
}
