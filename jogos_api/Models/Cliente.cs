using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace jogos_api.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string ClienteNome { get; set; } = null!;

    public string ClienteNomeUsuario { get; set; } = null!;

    public string ClienteEmail { get; set; } = null!;

    public string ClienteSenha { get; set; } = null!;

    public DateTime ClienteDataCadastro { get; set; }
    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
