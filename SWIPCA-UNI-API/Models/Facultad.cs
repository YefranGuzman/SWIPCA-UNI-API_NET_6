using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Facultad
{
    public int IdFacultad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int Vice { get; set; }

    public virtual ICollection<Aula> Aulas { get; } = new List<Aula>();

    public virtual ICollection<Carrera> Carreras { get; } = new List<Carrera>();

    public virtual ICollection<Departamento> Departamentos { get; } = new List<Departamento>();

    public virtual Usuario ViceNavigation { get; set; } = null!;
}
