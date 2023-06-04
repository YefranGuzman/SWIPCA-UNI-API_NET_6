using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Docente
{
    public int IdDocente { get; set; }

    public int IdUsuario { get; set; }

    public int IdDepartamento { get; set; }

    public int Disponibilidad { get; set; }

    public int TipoContrato { get; set; }

    public int Derpartamento { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<CargaAcademica> CargaAcademicas { get; } = new List<CargaAcademica>();

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();

    public virtual ICollection<Disponibilidad> Disponibilidads { get; } = new List<Disponibilidad>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Contrato TipoContratoNavigation { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
