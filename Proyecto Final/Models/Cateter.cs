namespace Proyecto.Models
{
    public class Cateter
    {
        public int Id { get; set; }
        public string TipoCateter { get; set; } 
        public string Material { get; set; }
        public string Longitud { get; set; } 
        public string Diametro { get; set; } 
        public string UsoPrevisto { get; set; }

        //relacion con pruebas
        public IEnumerable<Pruebas>? Pruebas { get; set; }
    }
}
