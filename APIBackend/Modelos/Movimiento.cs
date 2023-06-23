using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Movimiento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    [DefaultValue("")]
    public int Id { get; set; }

    public int? CuentaId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? TipoMovimiento { get; set; }

    public decimal? Valor { get; set; }

    public decimal? Saldo { get; set; }

    [JsonIgnore]
    public virtual Cuenta? oCuenta { get; set; } = null!;
}
