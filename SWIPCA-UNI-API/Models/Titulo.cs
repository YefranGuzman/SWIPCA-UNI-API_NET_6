using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Titulo
{
    public int IdDocente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Anio { get; set; }

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
