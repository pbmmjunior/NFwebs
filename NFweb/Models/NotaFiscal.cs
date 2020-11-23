using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFweb.Models
{
    public class NotaFiscal
    {
        public int IdNotaFiscal { get; set; }

        public Cliente cliente { get; set; }

        public Produto produto { get; set; }

        public Fornecedor fornecedor { get; set; }

        public int QtdeProdutos { get; set; }

        public DateTime DataNotaFiscal { get; set; }

        public double ValorNotaFiscal { get; set; }

        public IEnumerable<SelectListItem> ListaClientes { get; set; }

        public IEnumerable<SelectListItem> ListaProdutos { get; set; }

        public IEnumerable<SelectListItem> ListaFornecedor { get; set; }
    }
}