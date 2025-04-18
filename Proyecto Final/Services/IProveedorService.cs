using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<List<Proveedores>> ObtenerTodosAsync();
        Task<Proveedores?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Proveedores proveedor);
        Task ActualizarAsync(Proveedores proveedor);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
