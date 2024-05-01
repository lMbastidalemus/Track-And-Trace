using System;
using System.Collections.Generic;

namespace DL;

public partial class Repartidor
{
    public int IdRepartidor { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApelldioMaterno { get; set; } = null!;
}
