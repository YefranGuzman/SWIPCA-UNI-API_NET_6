﻿using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Carrera
{
    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdCarrera { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdFacultad { get; set; }

    public virtual ICollection<Grupo> Grupos { get; } = new List<Grupo>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual ICollection<Horario> Horarios { get; } = new List<Horario>();

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;
}
