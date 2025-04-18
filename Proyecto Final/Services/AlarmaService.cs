using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;

namespace Proyecto.Services
{
    public class AlarmaService : IAlarmaService
    {
        private readonly ApplicationDbContext _context;

        public AlarmaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Alarmas>> ObtenerTodasAsync()
        {
            return await _context.Alarmas.Include(a => a.Pruebas).ToListAsync();
        }

        public async Task<Alarmas?> ObtenerPorIdAsync(int id)
        {
            return await _context.Alarmas.Include(a => a.Pruebas).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CrearAsync(Alarmas alarma)
        {
            _context.Alarmas.Add(alarma);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Alarmas alarma)
        {
            _context.Update(alarma);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var alarma = await _context.Alarmas.FindAsync(id);
            if (alarma != null)
            {
                _context.Alarmas.Remove(alarma);
                await _context.SaveChangesAsync();
            }
        }

        public bool Existe(int id)
        {
            return _context.Alarmas.Any(a => a.Id == id);
        }
    }
}
