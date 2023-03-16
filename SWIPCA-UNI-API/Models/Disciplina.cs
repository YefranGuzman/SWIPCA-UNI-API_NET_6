using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Disciplina
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdDisciplina { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTitulo { get; set; }

    public int IdDepartamento { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Titulo IdTituloNavigation { get; set; } = null!;
}
