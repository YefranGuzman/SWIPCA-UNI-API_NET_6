namespace SWIPCA_UNI_API.Models
{
    public class Facultad
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idFacultad { get; set; }
        public string nombre { get; set; }
        public string recinto {get; set;}
        public int telefono { get; set;}
        public int extension { get; set;}
        public int idUsuario { get; set;}  
    }
}
