using Microsoft.AspNetCore.Identity;
using Proyecto_Final.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Documentos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public byte[] Archivo { get; set; }

        // Relación con ApplicationUser (AspNetUsers, antiguamente Usuarios)
        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }
    }
}