namespace SWIPCA_UNI_API.Models
{
    public class Carrera
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idCarrera { get; set; }
        public string nombre { get; set; }
        public int idFacultad { get; set; }
    }
}

