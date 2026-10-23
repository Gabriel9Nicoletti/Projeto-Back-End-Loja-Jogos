using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace jogos_api.Models;

public partial class Compra
{
    public int CompraId { get; set; }

    public int ClienteId { get; set; }

    public DateTime CompraData { get; set; }

    public string CompraSituacao { get; set; } = null!;
    [JsonIgnore]
    public virtual Cliente Cliente { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ItensCompra> ItensCompras { get; set; } = new List<ItensCompra>();
}
