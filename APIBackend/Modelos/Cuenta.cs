using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Cuenta
{
    [JsonIgnore]
    public int CuentaId { get; set; }

    public int? ClienteId { get; set; }

    public int? NumeroCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public decimal? SaldoInicial { get; set; }

    public string? Estado { get; set; }

    [JsonIgnore]
    public virtual Cliente? Cliente { get; set; }

    [JsonIgnore]
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
