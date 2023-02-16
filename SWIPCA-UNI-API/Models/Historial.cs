namespace SWIPCA_UNI_API.Models
{
    public class Historial
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idHorario { get; set; }
        public int idCarrera { get; set; }
        public int idClase { get; set; }
        public int idGrupo { get; set; }
    }
}
