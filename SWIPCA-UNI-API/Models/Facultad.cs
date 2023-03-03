using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Facultad
{
    public string UsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdFacultad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Recinto { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int? Extension { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Aula> Aulas { get; } = new List<Aula>();

    public virtual ICollection<Carrera> Carreras { get; } = new List<Carrera>();

    public virtual ICollection<Departamento> Departamentos { get; } = new List<Departamento>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Laboratorio> Laboratorios { get; } = new List<Laboratorio>();
}
