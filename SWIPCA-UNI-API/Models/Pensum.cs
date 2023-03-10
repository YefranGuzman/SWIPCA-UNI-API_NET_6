using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Pensum
{
    public int IdCarrera { get; set; }

    public int IdAsignatura { get; set; }

    public int IdDuraccion { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual Duraccion IdDuraccionNavigation { get; set; } = null!;
}
