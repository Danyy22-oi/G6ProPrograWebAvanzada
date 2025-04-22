using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using Proyecto_Final.Models;

namespace Proyecto_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alarmas> Alarmas { get; set; }
        public DbSet<Cateter> Cateter { get; set; }
        public DbSet<Correcciones> Correcciones { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Equipos> Equipos { get; set; }
        public DbSet<EstadoEquipos> EstadoEquipos { get; set; }

        public DbSet<Pruebas> Pruebas { get; set; }

        public DbSet<Materiales> Materiales { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Suministros> Suministros { get; set; }

        public DbSet<Documentos> Documentos { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //FK equipos con departamento
            modelBuilder.Entity<Equipos>().HasOne(x => x.Departamentos).WithMany(e => e.Equipos).HasForeignKey(f => f.DepartamentoId).HasConstraintName("FK_EQUIPOS_DEPA");

            //FK equipos con Estado equipo
            modelBuilder.Entity<Equipos>().HasOne(x => x.EstadoEquipos).WithMany(e => e.Equipos).HasForeignKey(f => f.EstadoEquipoId).HasConstraintName("FK_EQUIPOS_ESTADOEQUIPO");

            //FK pruebas con cateter
            modelBuilder.Entity<Pruebas>().HasOne(x => x.Cateter).WithMany(e => e.Pruebas).HasForeignKey(f => f.CateterId).HasConstraintName("FK_Pruebas_CATETER");

            //FK correcciones con pruebas
            modelBuilder.Entity<Correcciones>().HasOne(x => x.Pruebas).WithMany(e => e.Correcciones).HasForeignKey(f => f.PruebaId).HasConstraintName("FK_Correcciones_Pruebas");

            //FK alarmas con pruebas
            modelBuilder.Entity<Alarmas>().HasOne(x => x.Pruebas).WithMany(e => e.Alarmas).HasForeignKey(f => f.PruebaId).HasConstraintName("FK_ALARMAS_PRUEBAS");

            // Relación entre ApplicationUser y Departamentos
            modelBuilder.Entity<ApplicationUser>().HasOne(u => u.Departamentos).WithMany().HasForeignKey(u => u.DepartamentoId).HasConstraintName("FK_AspNetUsers_Departamentos");


            // Relación entre Documentos y AspNetUsers (Antigua Clase Usuarios)
            modelBuilder.Entity<Documentos>().HasOne(d => d.Usuario).WithMany(u => u.Documentos).HasForeignKey(d => d.UsuarioId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_Documentos_AspNetUsers");


        }

    }
}
