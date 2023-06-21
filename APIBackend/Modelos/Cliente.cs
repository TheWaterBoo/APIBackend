using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Contraseña { get; set; }

    public string? Estado { get; set; }

    public virtual Persona oPersona { get; set; } = null!;

    [JsonIgnore]
    public virtual Cuenta? Cuenta { get; set; }
}
