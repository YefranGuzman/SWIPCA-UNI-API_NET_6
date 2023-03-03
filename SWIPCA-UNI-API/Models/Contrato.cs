using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Contrato
{
    public int IdUsuario { get; set; }

    public int IdContrato { get; set; }

    public string Tipo { get; set; } = null!;

    public TimeSpan HorasLaboral { get; set; }

    public int Estado { get; set; }

    public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
