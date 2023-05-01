using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Periodo
{
    public int IdPeriodo { get; set; }

    public int IdTurno { get; set; }

    public string Nombre { get; set; } = null!;

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFinal { get; set; }

    public virtual ICollection<Disponibilidad> Disponibilidads { get; } = new List<Disponibilidad>();

    public virtual Turno IdTurnoNavigation { get; set; } = null!;
}
