using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class AulaLaboratorio
{
    public int IdAuLa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int IdFacultad { get; set; }

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;
}
