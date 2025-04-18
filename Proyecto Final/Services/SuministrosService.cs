using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class SuministrosService : ISuministrosService
    {
        private readonly ApplicationDbContext _context;

        public SuministrosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Suministros>> ObtenerTodosAsync()
        {
            return await _context.Suministros.ToListAsync();
        }

        public async Task<Suministros?> ObtenerPorIdAsync(int id)
        {
            return await _context.Suministros.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CrearAsync(Suministros suministro)
        {
            _context.Suministros.Add(suministro);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Suministros suministro)
        {
            _context.Update(suministro);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var suministro = await _context.Suministros.FindAsync(id);
            if (suministro != null)
            {
                _context.Suministros.Remove(suministro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Suministros.AnyAsync(s => s.Id == id);
        }
    }
}
