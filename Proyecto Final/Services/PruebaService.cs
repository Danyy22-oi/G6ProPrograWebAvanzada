using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Data;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class PruebaService : IPruebaService
    {
        private readonly ApplicationDbContext _context;

        public PruebaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pruebas>> ObtenerTodasAsync()
        {
            return await _context.Pruebas.Include(p => p.Cateter).ToListAsync();
        }

        public async Task<Pruebas?> ObtenerPorIdAsync(int id)
        {
            return await _context.Pruebas.Include(p => p.Cateter).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pruebas?> ObtenerPorIdSimpleAsync(int id)
        {
            return await _context.Pruebas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CrearAsync(Pruebas prueba)
        {
            _context.Pruebas.Add(prueba);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Pruebas prueba)
        {
            _context.Update(prueba);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var prueba = await _context.Pruebas.FindAsync(id);
            if (prueba != null)
            {
                _context.Pruebas.Remove(prueba);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Pruebas.AnyAsync(p => p.Id == id);
        }
    }
}
