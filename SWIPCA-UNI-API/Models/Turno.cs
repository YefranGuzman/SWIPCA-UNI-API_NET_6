using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string Nombre { get; set; } = null!;

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFinal { get; set; }

    public string Dia { get; set; } = null!;

    public virtual ICollection<Grupo> Grupos { get; } = new List<Grupo>();
}
