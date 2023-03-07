using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public int TipoRol { get; set; }

    public string Email { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Nick { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public virtual ICollection<Departamento> Departamentos { get; } = new List<Departamento>();

    public virtual ICollection<Facultad> Facultads { get; } = new List<Facultad>();

    public virtual Rol TipoRolNavigation { get; set; } = null!;
}
