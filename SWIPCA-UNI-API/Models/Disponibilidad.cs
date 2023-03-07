using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Disponibilidad
{
    public int IdDisponibilidad { get; set; }

    public int IdDocente { get; set; }

    public string Observacíon { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int Periodicidad { get; set; }

    public string? Evidencia { get; set; }

    public int Estado { get; set; }

    public int TipoJustificación { get; set; }

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
