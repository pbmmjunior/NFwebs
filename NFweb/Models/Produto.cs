using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFweb.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }

        public Fornecedor fornecedor { get; set; }

        public string NomeProduto { get; set; }

        public double ValorProduto { get; set; }

    }
}