using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Cuenta
{
    public int Id { get; set; }

    public string? NumeroCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public int? SaldoInicial { get; set; }

    public string? Estado { get; set; }

    [JsonIgnore]
    public virtual Cliente? oCliente { get; set; } = null!;

    [JsonIgnore]
    public virtual Movimiento? Movimiento { get; set; }
}
