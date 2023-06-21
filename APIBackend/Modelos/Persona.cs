using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBackend.Modelos;

public partial class Persona
{
    [JsonIgnore]
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Genero { get; set; }

    public int? Edad { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    [JsonIgnore]
    public virtual Cliente? Cliente { get; set; }
}
