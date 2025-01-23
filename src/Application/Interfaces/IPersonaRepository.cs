using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> GetByIdAsync(Guid userId);
        Task<Persona> GetByEmailAsync(string email);
        IQueryable<Persona> GetAllAsync(string filtro);

        // Método para contar el número total de usuarios
        Task<int> CountAsync(string filtro);
        Task AddAsync(Persona user);
        Task UpdateAsync(Persona user);
        Task DeleteAsync(Guid userId);
    }
}
