namespace SWIPCA_UNI_API.Models
{
    public class Horario
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idHistorial { get; set; }
        public int idAsignatura { get; set;}
        public int idDocente { get; set;}
        public int idCarrera { get;set;}
        public int frecuencia { get;set;}
    }
}
