using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public string IdentificadorContrato { get; set; } = null!;

    public string TipoContrato { get; set; } = null!;

    public int HorasMinimas { get; set; }
    public int HorasMaximas { get; set; }

    //public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();
}
