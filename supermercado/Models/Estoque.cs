using System;
using System.Collections.Generic;

#nullable disable

namespace supermercado.Models
{
    public partial class Estoque
    {
        public int CodEstoque { get; set; }
        public int CodProduto { get; set; }
        public int QuantidadeDisponivel { get; set; }

        public virtual Produto CodProdutoNavigation { get; set; }
    }
}
