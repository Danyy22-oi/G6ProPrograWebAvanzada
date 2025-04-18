using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IPruebaService
    {
        Task<List<Pruebas>> ObtenerTodasAsync();
        Task<Pruebas?> ObtenerPorIdAsync(int id);
        Task<Pruebas?> ObtenerPorIdSimpleAsync(int id);
        Task CrearAsync(Pruebas prueba);
        Task ActualizarAsync(Pruebas prueba);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
