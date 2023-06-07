using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Carrera
{
    public int IdCarrera { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdFacultad { get; set; }

    public int IdDuraccion { get; set; }

    public int Duracion { get; set; }

    public virtual ICollection<CargaAcademica> CargaAcademicas { get; } = new List<CargaAcademica>();

    public virtual ICollection<Grupo> Grupos { get; } = new List<Grupo>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual ICollection<Horario> Horarios { get; } = new List<Horario>();

    public virtual Duraccion IdDuraccionNavigation { get; set; } = null!;

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;
}
