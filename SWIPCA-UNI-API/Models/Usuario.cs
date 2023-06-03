using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string NormalizedUserName { get; set; } = null!;

    public string? RoleId { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public int TipoRol { get; set; }

    public string Celular { get; set; } = null!;

    public string Nick { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int? Estado { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; } = new List<Departamento>();

    public virtual ICollection<Facultad> Facultads { get; } = new List<Facultad>();

    public virtual Docente IdUsuarioNavigation { get; set; } = null!;

    public virtual Rol TipoRolNavigation { get; set; } = null!;
}
