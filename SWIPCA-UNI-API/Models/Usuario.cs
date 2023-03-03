using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Usuario
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdUsuario { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public int IdRol { get; set; }

    public string Email { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int Estado { get; set; }

    public virtual ICollection<Contrato> Contratos { get; } = new List<Contrato>();

    public virtual ICollection<Departamento> Departamentos { get; } = new List<Departamento>();

    public virtual ICollection<Facultad> Facultads { get; } = new List<Facultad>();

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
