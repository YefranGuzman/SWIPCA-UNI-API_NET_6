using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Rol
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdRol { get; set; }

    public string Nombrerol { get; set; } = null!;

    public int Estado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
