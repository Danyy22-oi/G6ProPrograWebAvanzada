using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto.Models;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly ApplicationDbContext _context;

        public ProveedorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedores>> ObtenerTodosAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }

        public async Task<Proveedores?> ObtenerPorIdAsync(int id)
        {
            return await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CrearAsync(Proveedores proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Proveedores proveedor)
        {
            _context.Update(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Proveedores.AnyAsync(p => p.Id == id);
        }
    }
}
