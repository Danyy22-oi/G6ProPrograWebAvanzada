using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IMaterialesService
    {
        Task<List<Materiales>> ObtenerTodosAsync();
        Task<Materiales?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Materiales material);
        Task ActualizarAsync(Materiales material);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
