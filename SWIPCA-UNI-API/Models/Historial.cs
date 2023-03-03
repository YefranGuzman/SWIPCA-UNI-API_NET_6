using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Historial
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdHistorial { get; set; }

    public int IdAsignatura { get; set; }

    public int IdDocente { get; set; }

    public int IdCarrera { get; set; }

    public int Frecuencia { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
