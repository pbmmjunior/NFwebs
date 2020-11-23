using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFweb.Models;


namespace NFweb.Controllers
{
    public class NotaFiscalController : Controller
    {
        // GET: NotaFiscal
        public ActionResult Index(string msgSucesso = "")
        { 
            using (NotaFiscalModel model = new NotaFiscalModel())
            {
                List<NotaFiscal> lista = model.ListarNota();
                if (!string.IsNullOrEmpty(msgSucesso))
                {
                    ViewBag.MsgSucesso = msgSucesso;
                }
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            NotaFiscal model = new NotaFiscal();
            model.produto = new Produto();
            model.cliente = new Cliente();
            model.fornecedor = new Fornecedor();
            List<SelectListItem> clientes = new List<SelectListItem>();
            List<SelectListItem> fornecedores = new List<SelectListItem>();
            List<SelectListItem> produtos = new List<SelectListItem>();
            using (NotaFiscalModel acoes = new NotaFiscalModel())
            {
                clientes = acoes.ListarClientes();
                fornecedores = acoes.ListarFornecedores();
                produtos = acoes.ListarProdutos(new Fornecedor());
                if (id > 0)
                {
                    model = acoes.ObterNota(id);
                }
            }
            model.ListaClientes = clientes;
            model.ListaProdutos = produtos;
            model.ListaFornecedor = fornecedores;
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(NotaFiscal nota)
        {
            using (NotaFiscalModel model = new NotaFiscalModel())
            {
                nota.DataNotaFiscal = DateTime.Now;
                if(nota.IdNotaFiscal > 0)
                {
                    model.AtualizarNota(nota);
                }
                else
                {
                    model.IncluirNota(nota);
                }
            }

            return Json("Item salvo com sucesso", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(NotaFiscal nota)
        {
            using (NotaFiscalModel model = new NotaFiscalModel())
            {
                model.ExcluirNota(nota);

                return Json("Item removido com sucesso", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Relatorio(int id = 0)
        {
            List<NotaFiscal> lista = new List<NotaFiscal>();

            using (NotaFiscalModel acoes = new NotaFiscalModel())
            {
                if (id > 0)
                {
                    lista = acoes.ListarNotasPorProduto(id);
                }
            }

            return View("Relatorio", lista);
        }

    }
}