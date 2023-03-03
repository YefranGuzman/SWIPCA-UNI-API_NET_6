using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdCarrera { get; set; }

    public int IdTurno { get; set; }

    public virtual ICollection<Horario> Horarios { get; } = new List<Horario>();

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual Turno IdTurnoNavigation { get; set; } = null!;
}
