using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace jogos_api.Models;

public partial class Jogo
{
    public int JogoId { get; set; }

    public int GeneroId { get; set; }

    public string JogoTitulo { get; set; } = null!;

    public string JogoDescricao { get; set; } = null!;

    public decimal JogoPreco { get; set; }

    public DateTime JogoDataLancamento { get; set; }

    public string JogoDesenvolvedora { get; set; } = null!;

    public string JogoClassificacaoIndicativa { get; set; } = null!;

    [JsonIgnore]
    public virtual Genero Genero { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ItensCompra> ItensCompras { get; set; } = new List<ItensCompra>();
}
