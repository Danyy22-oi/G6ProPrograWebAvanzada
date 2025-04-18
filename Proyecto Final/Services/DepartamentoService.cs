using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Data;
using Proyecto.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly ApplicationDbContext _context;

        public DepartamentoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Departamentos>> ObtenerTodosAsync()
        {
            return await _context.Departamentos.ToListAsync();
        }

        public async Task<Departamentos?> ObtenerPorIdAsync(int id)
        {
            return await _context.Departamentos.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task CrearAsync(Departamentos departamento)
        {
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Departamentos departamento)
        {
            _context.Update(departamento);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Departamentos.AnyAsync(d => d.Id == id);
        }
    }
}
