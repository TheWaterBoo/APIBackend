using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Cuenta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    [DefaultValue("")]
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public int? NumeroCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public decimal? SaldoInicial { get; set; }

    public string? Estado { get; set; }

    [JsonIgnore]
    public virtual Cliente? oCliente { get; set; } = null!;

    [JsonIgnore]
    public virtual Movimiento? Movimiento { get; set; }
}
