using System;
using System.Collections.Generic;

#nullable disable

namespace supermercado.Models
{
    public partial class Funcionario
    {
        public int CodFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        public string Setor { get; set; }
        public string Cagor { get; set; }
    }
}
