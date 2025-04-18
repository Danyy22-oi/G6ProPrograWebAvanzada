using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface ICateterService
    {
        Task<List<Cateter>> ObtenerTodosAsync();
        Task<Cateter?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Cateter cateter);
        Task ActualizarAsync(Cateter cateter);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
