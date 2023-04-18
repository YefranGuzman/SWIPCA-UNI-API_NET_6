using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdFacultad { get; set; }

    public int Jefe { get; set; }

    public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;

    public virtual Usuario JefeNavigation { get; set; } = null!;
}
