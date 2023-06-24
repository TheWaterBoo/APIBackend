using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Movimiento
{
    [JsonIgnore]
    public int MovimientoId { get; set; }

    public int? CuentaId { get; set; }

    public DateTime? FechaMovimiento { get; set; }

    public string? TipoMovimiento { get; set; }

    public decimal? ValorMovimiento { get; set; }

    public decimal? SaldoDisponible { get; set; }

    [JsonIgnore]
    public virtual Cuenta? Cuenta { get; set; }
}
