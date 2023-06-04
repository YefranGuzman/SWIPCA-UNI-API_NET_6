using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SWIPCA_UNI_API.Models;

public partial class Rol : IdentityRole<int>
{
    public int IdRol { get; set; }

    public string Titulo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
