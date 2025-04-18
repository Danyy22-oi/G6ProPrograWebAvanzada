namespace Proyecto.Models
{
    public class Departamentos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public string Supervisor { get; set; } 
        public string Area { get; set; } = string.Empty;

        //RELACION A EQUIPOS
        public IEnumerable<Equipos>? Equipos { get; set; }

    }
}
