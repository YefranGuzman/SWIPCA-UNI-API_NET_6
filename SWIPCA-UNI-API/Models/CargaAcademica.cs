using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SWIPCA_UNI_API.Models;

public partial class CargaAcademica
{
    [BindNever]
    public int IdCaHo { get; set; }

    public int IdCarrera { get; set; }

    public int IdClase { get; set; }

    public int IdGrupo { get; set; }

    public int IdDocente { get; set; }

    public int IdJefe { get; set; }

    public int Estado { get; set; }

    public string? Observacion { get; set; }
}
