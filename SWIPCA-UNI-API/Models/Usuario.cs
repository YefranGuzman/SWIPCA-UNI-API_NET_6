namespace SWIPCA_UNI_API.Models
{
    public class Usuario
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int idUsuario { get; set; }
        public string primernombre { get; set; }
        public string segundonombre { get; set;}
        public string primerapellido { get; set;}
        public string segundoapellido { get;set;}
        public int idRol { get; set; }
        public string email { get; set; }
        public int celular { get; set; }
        public string contrasena { get; set; }
        public int Estado { get; set; }
    }
}
