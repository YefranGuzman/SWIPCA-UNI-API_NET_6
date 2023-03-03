using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Grupo> Grupos { get; } = new List<Grupo>();

    public virtual ICollection<Periodo> Periodos { get; } = new List<Periodo>();
}
