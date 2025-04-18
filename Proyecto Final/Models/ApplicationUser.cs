using Microsoft.AspNetCore.Identity;
using Proyecto.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Proyecto_Final.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int DepartamentoId { get; set; }

        // Relación con Departamentos.
        public virtual Departamentos Departamentos { get; set; }

        // Relación con Documentos (un usuario puede tener muchos documentos).
        public virtual ICollection<Documentos> Documentos { get; set; }
    }
}