using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Docente
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdDocente { get; set; }

    public int IdUsuario { get; set; }

    public int IdContrato { get; set; }

    public int IdDepartamento { get; set; }

    public int Estado { get; set; }

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();

    public virtual ICollection<Disponibilidad> Disponibilidads { get; } = new List<Disponibilidad>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual Contrato IdContratoNavigation { get; set; } = null!;

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
