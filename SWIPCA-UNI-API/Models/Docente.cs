namespace SWIPCA_UNI_API.Models
{
    public class Docente
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idDocente { get; set; }
        public int idUsuario { get; set; }
        public int idContrato { get; set; }
        public int idDepartamento { get; set; }
        public int Estado { get; set; }
    }
}
