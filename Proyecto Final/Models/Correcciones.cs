namespace Proyecto.Models
{
    public class Correcciones
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } 
        public DateTime Fecha { get; set; }

        //RELACION CON PRUEBAS
        public int PruebaId { get; set; }
        public Pruebas? Pruebas { get; set; }
    }
}
