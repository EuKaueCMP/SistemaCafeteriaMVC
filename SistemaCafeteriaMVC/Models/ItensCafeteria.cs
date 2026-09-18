using System;
using System.Collections.Generic;

namespace SistemaCafeteriaMVC.Models;

public partial class ItensCafeteria
{
    public int ItemId { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public decimal Preco { get; set; }
}
