using System;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Models;

namespace Galaxy.Planets.Core.Interfaces
{
    public interface IGrpcService<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<ActionResponse> UpdateStatusAsync(T team);
    }
}