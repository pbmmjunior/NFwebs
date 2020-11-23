using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFweb.Models
{
    public class Fornecedor
    {
        public int IdFornecedor { get; set; }

        public string NomeFornecedor { get; set; }

        public string CNPJFornecedor { get; set; }

        public string EnderecoFornecedor { get; set; }
    }
}