using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Area
{
    public int IdArea { get; set; }

    public string Diciplina { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Asignatura> Asignaturas { get; } = new List<Asignatura>();
}
