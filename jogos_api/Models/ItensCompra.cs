using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace jogos_api.Models;

public partial class ItensCompra
{
    public int ItemCompraId { get; set; }

    public int CompraId { get; set; }

    public int JogoId { get; set; }

    public decimal ItemCompraPreco { get; set; }

    [JsonIgnore]
    public virtual Compra Compra { get; set; } = null!;
    [JsonIgnore]
    public virtual Jogo Jogo { get; set; } = null!;
}
