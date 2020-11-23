using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFweb.Models
{
    public class Relatorio
    {
        public Produto produto { get; set; }

        public NotaFiscal nota { get; set; }
    }
}