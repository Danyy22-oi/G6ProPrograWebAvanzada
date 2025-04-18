using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IEstadoEquiposService
    {
        Task<List<EstadoEquipos>> ObtenerTodosAsync();
        Task<EstadoEquipos?> ObtenerPorIdAsync(int id);
        Task CrearAsync(EstadoEquipos estado);
        Task ActualizarAsync(EstadoEquipos estado);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
