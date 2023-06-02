using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public int IdAsignatura { get; set; }

    public int IdDocente { get; set; }

    public int Docente { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFinal { get; set; }

    public string Dia { get; set; } = null!;

    public virtual ICollection<Horario> Horarios { get; } = new List<Horario>();

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
