using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public int IdCarrera { get; set; }

    public int IdClase { get; set; }

    public int IdGrupo { get; set; }

    public virtual Carrera IdCarreraNavigation { get; set; }

    public virtual Grupo IdGrupoNavigation { get; set; }
}
