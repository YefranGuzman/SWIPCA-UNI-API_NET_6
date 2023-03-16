using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public string Tipo { get; set; } = null!;

    public TimeSpan HorasLaboral { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Jornada { get; set; } = null!;

    public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();
}
