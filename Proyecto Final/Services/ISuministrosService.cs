using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface ISuministrosService
    {
        Task<List<Suministros>> ObtenerTodosAsync();
        Task<Suministros?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Suministros suministro);
        Task ActualizarAsync(Suministros suministro);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
