using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFweb.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string NomeCliente { get; set; }

        public string CNPJCliente { get; set; }
        
        public string EnderecoCliente { get; set; }
    }
}