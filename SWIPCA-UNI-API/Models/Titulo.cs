using System;
using System.Collections.Generic;

namespace SWIPCA_UNI_API.Models;

public partial class Titulo
{
    public int IdTitulo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Disciplina> Disciplinas { get; } = new List<Disciplina>();
}
