using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Cliente
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int? ClienteId { get; set; }

    public int? PersonaId { get; set; }

    public string? Contraseña { get; set; }

    public string? Estado { get; set; }

    public virtual Persona oPersona { get; set; } = null!;

    [JsonIgnore]
    public virtual Cuenta? Cuenta { get; set; }
}
