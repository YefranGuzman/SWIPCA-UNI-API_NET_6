namespace SWIPCA_UNI_API.Models
{
    public class Clase
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int IdClase { get; set; }
        public int IdDocente { get; set; }
        public DateTime? Horainicio { get; set; }
        public DateTime? Horafinal { get; set; }
        public string? Dia { get; set; }

    }
}
