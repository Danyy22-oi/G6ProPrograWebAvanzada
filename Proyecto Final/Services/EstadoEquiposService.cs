using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class EstadoEquiposService : IEstadoEquiposService
    {
        private readonly ApplicationDbContext _context;

        public EstadoEquiposService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstadoEquipos>> ObtenerTodosAsync()
        {
            return await _context.EstadoEquipos.ToListAsync();
        }

        public async Task<EstadoEquipos?> ObtenerPorIdAsync(int id)
        {
            return await _context.EstadoEquipos.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CrearAsync(EstadoEquipos estado)
        {
            _context.EstadoEquipos.Add(estado);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(EstadoEquipos estado)
        {
            _context.Update(estado);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var estado = await _context.EstadoEquipos.FindAsync(id);
            if (estado != null)
            {
                _context.EstadoEquipos.Remove(estado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.EstadoEquipos.AnyAsync(e => e.Id == id);
        }
    }
}
