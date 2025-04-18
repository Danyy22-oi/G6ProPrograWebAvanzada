namespace Proyecto.Models
{
    public class EstadoEquipos
    {

        public int Id { get; set; }
        public string Estado { get; set; } 

        //relacion con equipos
        public IEnumerable<Equipos>? Equipos { get; set; }
    }
}
