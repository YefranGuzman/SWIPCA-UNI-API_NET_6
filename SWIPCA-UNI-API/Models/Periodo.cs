using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Periodo
{
    public int IdPeriodo { get; set; }

    public string IdentificadorPeriodo { get; set; } = null!;

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFinal { get; set; }

    public int IdTurno { get; set; }
}
