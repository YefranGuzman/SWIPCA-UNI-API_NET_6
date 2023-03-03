using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Departamento
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdFacultad { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Asignatura> Asignaturas { get; } = new List<Asignatura>();

    public virtual ICollection<Disciplina> Disciplinas { get; } = new List<Disciplina>();

    public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
