namespace Proyecto.Models
{
    public class Equipos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public DateTime FechaMantenimientoPreventivo { get; set; }
        public DateTime FechaCalibracion { get; set; }
        public int Cantidad { get; set; }
        public string Ubicacion { get; set; } 


        //relacion con departamento 
        public int DepartamentoId { get; set; }
        public Departamentos? Departamentos { get; set; }

        //RELACION CON ESTADO EQUIPOS
        public int EstadoEquipoId { get; set; }
        public EstadoEquipos? EstadoEquipos { get; set; }
    }
}
