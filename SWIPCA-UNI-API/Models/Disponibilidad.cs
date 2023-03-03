using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Disponibilidad
{
    public int IdDisponibilidad { get; set; }

    public int IdDocente { get; set; }

    public int IdPeriodo { get; set; }

    public string Dia { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    public virtual Periodo IdPeriodoNavigation { get; set; } = null!;
}
