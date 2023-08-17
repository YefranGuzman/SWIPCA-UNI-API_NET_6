using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Aula
{
    public int IdAuLa { get; set; }

    public string NombreAula { get; set; } = null!;

    public int IdFacultad { get; set; }

    //public virtual Facultad IdFacultadNavigation { get; set; } = null!;
}
