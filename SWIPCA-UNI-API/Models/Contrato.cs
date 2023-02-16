namespace SWIPCA_UNI_API.Models
{
    public class Contrato
    {
        public int idUsuario { get; set; }
        public int idContrato { get; set; }
        public string tipo { get; set; }
        public TimeSpan horasLaboral { get; set; }
        public int Estado { get;set; }
    }
}
