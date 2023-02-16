namespace SWIPCA_UNI_API.Models
{
    public class Disciplina
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idDisciplina { get; set; }
        public string nombre { get; set; }
        public int idTitulo { get; set; }
        public int idDepartamento { get; set; }
    }
}
