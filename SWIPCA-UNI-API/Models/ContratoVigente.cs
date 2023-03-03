using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class ContratoVigente
{
    public int IdContrato { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Jornada { get; set; } = null!;

    public TimeSpan Hora { get; set; }
}
