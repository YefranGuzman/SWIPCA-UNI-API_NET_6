using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Aula
{
    public int IdAula { get; set; }

    public string Nombre { get; set; } = null!;

    public string? NumeroAula { get; set; }

    public int IdFacultad { get; set; }

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;
}
