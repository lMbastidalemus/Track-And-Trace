using System;
using System.Collections.Generic;

namespace DL;

public partial class Paquete
{
    public int IdPaquete { get; set; }

    public string InstruccionEntrega { get; set; } = null!;

    public decimal Peso { get; set; }

    public string DireccionOrigen { get; set; } = null!;

    public string DireccionEntrega { get; set; } = null!;

    public DateTime FechaEstimadaEntrega { get; set; }

    public string NumeroGuia { get; set; } = null!;

    public byte[]? CodigoQr { get; set; }
}
