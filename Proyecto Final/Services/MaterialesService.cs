using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class MaterialesService : IMaterialesService
    {
        private readonly ApplicationDbContext _context;

        public MaterialesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Materiales>> ObtenerTodosAsync()
        {
            return await _context.Materiales.ToListAsync();
        }

        public async Task<Materiales?> ObtenerPorIdAsync(int id)
        {
            return await _context.Materiales.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CrearAsync(Materiales material)
        {
            _context.Materiales.Add(material);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Materiales material)
        {
            _context.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var material = await _context.Materiales.FindAsync(id);
            if (material != null)
            {
                _context.Materiales.Remove(material);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Materiales.AnyAsync(m => m.Id == id);
        }
    }
}
