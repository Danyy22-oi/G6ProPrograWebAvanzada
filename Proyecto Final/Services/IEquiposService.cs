using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IEquiposService
    {
        Task<List<Equipos>> ObtenerTodosAsync();
        Task<Equipos?> ObtenerPorIdAsync(int id);
        Task<Equipos?> ObtenerPorIdSimpleAsync(int id);
        Task CrearAsync(Equipos equipo);
        Task ActualizarAsync(Equipos equipo);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
