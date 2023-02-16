namespace SWIPCA_UNI_API.Models
{
    public class Departamentocs
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idDepartamento { get; set; }
        public string nombre { get; set; }
        public int idFacultad { get; set; }
        public int idUsuario { get; set; }
    }
}
