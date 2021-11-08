using System;
using System.Collections.Generic;

#nullable disable

namespace supermercado.Models
{
    public partial class Produto
    {
        public Produto()
        {
            Estoques = new HashSet<Estoque>();
        }

        public int CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public int Valor { get; set; }
        public int Quantidade { get; set; }

        public virtual ICollection<Estoque> Estoques { get; set; }
    }
}
