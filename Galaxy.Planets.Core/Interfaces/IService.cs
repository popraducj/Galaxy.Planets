using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Models;

namespace Galaxy.Planets.Core.Interfaces
{
    public interface IService<T>
    {
        Task<ActionResponse> AddAsync(T model);
        Task<ActionResponse> UpdateAsync(T model);
        Task<List<T>> GetAllAsync();
        T GetById(Guid id);
    }
}