using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Cliente
{
    [JsonIgnore]
    public int ClienteId { get; set; }

    public int? PersonaId { get; set; }

    public string? Contrasena { get; set; }

    public string? Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual Persona? Persona { get; set; }
}
