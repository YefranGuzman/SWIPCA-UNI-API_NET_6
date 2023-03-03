using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Asignatura
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdAsignatura { get; set; }

    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int Frecuencia { get; set; }

    public int Estado { get; set; }

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
