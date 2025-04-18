using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IDepartamentoService
    {
        Task<List<Departamentos>> ObtenerTodosAsync();
        Task<Departamentos?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Departamentos departamento);
        Task ActualizarAsync(Departamentos departamento);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
