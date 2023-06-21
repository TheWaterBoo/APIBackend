using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Movimiento
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? TipoMovimiento { get; set; }

    public int? Valor { get; set; }

    [JsonIgnore]
    public int? Saldo { get; set; }

    [JsonIgnore]
    public virtual Cuenta? oCuenta { get; set; } = null!;
}
