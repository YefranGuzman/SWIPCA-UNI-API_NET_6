using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Laboratorio
{
    public int IdLaboratorio { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdFacultad { get; set; }

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;
}
