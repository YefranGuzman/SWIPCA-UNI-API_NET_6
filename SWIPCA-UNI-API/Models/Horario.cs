using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Horario
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdHorario { get; set; }

    public int IdCarrera { get; set; }

    public int IdClase { get; set; }

    public int IdGrupo { get; set; }

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual Clase IdClaseNavigation { get; set; } = null!;

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
