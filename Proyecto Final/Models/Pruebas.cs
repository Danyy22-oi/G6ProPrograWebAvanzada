using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Pruebas
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } 
        public DateTime Fecha { get; set; }
        public int PresionBalloon { get; set; }
        public bool InspeccionTactil { get; set; }
        public int RupturaBalloon { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Desinflado { get; set; }
        public bool Estado { get; set; }

        //RELACION CON cateter
        public int CateterId {  get; set; }
        public Cateter? Cateter { get; set; }

        //relacion con correcciones
        public IEnumerable<Correcciones>? Correcciones { get; set; }

        //relacion con alarmas
        public IEnumerable<Alarmas>? Alarmas { get; set; }

    }
}
