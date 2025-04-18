using Proyecto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services.Interfaces
{
    public interface IAlarmaService
    {
        Task<List<Alarmas>> ObtenerTodasAsync();
        Task<Alarmas?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Alarmas alarma);
        Task ActualizarAsync(Alarmas alarma);
        Task EliminarAsync(int id);
        bool Existe(int id);
    }
}
