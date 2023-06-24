using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Persona
{
    [JsonIgnore]
    public int PersonaId { get; set; }

    public string? Nombre { get; set; }

    public string? Genero { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
