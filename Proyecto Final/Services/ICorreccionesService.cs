using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface ICorreccionesService
    {
        Task<List<Correcciones>> ObtenerTodasAsync();
        Task<Correcciones?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Correcciones correccion);
        Task ActualizarAsync(Correcciones correccion);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
