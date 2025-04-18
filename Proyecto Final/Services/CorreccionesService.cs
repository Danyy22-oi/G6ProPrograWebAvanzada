using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class CorreccionesService : ICorreccionesService
    {
        private readonly ApplicationDbContext _context;

        public CorreccionesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Correcciones>> ObtenerTodasAsync()
        {
            return await _context.Correcciones.Include(c => c.Pruebas).ToListAsync();
        }

        public async Task<Correcciones?> ObtenerPorIdAsync(int id)
        {
            return await _context.Correcciones.Include(c => c.Pruebas).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CrearAsync(Correcciones correccion)
        {
            _context.Correcciones.Add(correccion);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Correcciones correccion)
        {
            _context.Update(correccion);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var correccion = await _context.Correcciones.FindAsync(id);
            if (correccion != null)
            {
                _context.Correcciones.Remove(correccion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Correcciones.AnyAsync(e => e.Id == id);
        }
    }
}

