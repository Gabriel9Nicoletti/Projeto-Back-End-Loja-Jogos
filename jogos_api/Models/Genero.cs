using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace jogos_api.Models;

public partial class Genero
{
    public int GeneroId { get; set; }

    public string GeneroNome { get; set; } = null!;

    public string? GeneroDescricao { get; set; }
    [JsonIgnore]
    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
