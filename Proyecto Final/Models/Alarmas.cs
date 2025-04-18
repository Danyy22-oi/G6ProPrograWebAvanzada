namespace Proyecto.Models
{
    public class Alarmas
    {
        public int Id { get; set; }
        public string Tipo { get; set; } 
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Investigacion { get; set; }


        //relacion con pruebas
        public int PruebaId { get; set; }
        public Pruebas? Pruebas { get; set; }
    }
}
