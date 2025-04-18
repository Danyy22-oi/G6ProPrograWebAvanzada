using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class EquiposService : IEquiposService
    {
        private readonly ApplicationDbContext _context;

        public EquiposService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipos>> ObtenerTodosAsync()
        {
            return await _context.Equipos
                .Include(e => e.Departamentos)
                .Include(e => e.EstadoEquipos)
                .ToListAsync();
        }

        public async Task<Equipos?> ObtenerPorIdAsync(int id)
        {
            return await _context.Equipos
                .Include(e => e.Departamentos)
                .Include(e => e.EstadoEquipos)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Equipos?> ObtenerPorIdSimpleAsync(int id)
        {
            return await _context.Equipos.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CrearAsync(Equipos equipo)
        {
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Equipos equipo)
        {
            _context.Update(equipo);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Equipos.AnyAsync(e => e.Id == id);
        }
    }
}
