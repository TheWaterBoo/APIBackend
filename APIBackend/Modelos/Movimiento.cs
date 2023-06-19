using System;
using System.Collections.Generic;

namespace APIBackend.Modelos;

public partial class Movimiento
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? TipoMovimiento { get; set; }

    public int? Valor { get; set; }

    public int? Saldo { get; set; }
}
