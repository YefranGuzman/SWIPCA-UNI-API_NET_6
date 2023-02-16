namespace SWIPCA_UNI_API.Models
{
    public class Rol
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idRol { get; set; }
        public string nombrerol { get; set; }
        public int Estado { get; set; }
    }
}
