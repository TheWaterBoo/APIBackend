using System;
using System.Collections.Generic;

namespace APIBackend.Modelos;

public partial class Cuenta
{
    public int Id { get; set; }

    public string? NumeroCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public int? SaldoInicial { get; set; }

    public string? Estado { get; set; }
}
