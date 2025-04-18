using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Data;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class CateterService : ICateterService
    {
        private readonly ApplicationDbContext _context;

        public CateterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cateter>> ObtenerTodosAsync()
        {
            return await _context.Cateter.ToListAsync();
        }

        public async Task<Cateter?> ObtenerPorIdAsync(int id)
        {
            return await _context.Cateter.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CrearAsync(Cateter cateter)
        {
            _context.Cateter.Add(cateter);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Cateter cateter)
        {
            _context.Update(cateter);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var cateter = await _context.Cateter.FindAsync(id);
            if (cateter != null)
            {
                _context.Cateter.Remove(cateter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Cateter.AnyAsync(e => e.Id == id);
        }
    }
}

