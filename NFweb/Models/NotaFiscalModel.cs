using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.Globalization;

namespace NFweb.Models
{
    public class NotaFiscalModel : IDisposable
    {
        private SqlConnection connection;
        public NotaFiscalModel()
        {
            //connection = new SqlConnection(strConn);
            //connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<NotaFiscal> ListarNota()
        {
            List<NotaFiscal> lista = new List<NotaFiscal>();

            try
            {
                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarNotaFiscal", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    var cultura = new CultureInfo("pt-BR");
                    while (reader.Read())
                    {
                        NotaFiscal nota = new NotaFiscal();
                        nota.cliente = new Cliente();
                        nota.produto = new Produto();
                        nota.fornecedor = new Fornecedor();
                        nota.IdNotaFiscal = int.Parse(reader["NF_ID"].ToString());
                        nota.cliente.IdCliente = int.Parse(reader["CLI_ID"].ToString());
                        nota.cliente.NomeCliente = reader["CLI_NOME"].ToString();
                        nota.produto.IdProduto = int.Parse(reader["PROD_ID"].ToString());
                        nota.produto.NomeProduto = reader["PROD_NOME"].ToString();
                        nota.fornecedor.IdFornecedor = int.Parse(reader["FOR_ID"].ToString());
                        nota.fornecedor.NomeFornecedor = reader["FOR_NOME"].ToString();
                        nota.QtdeProdutos = int.Parse(reader["NF_QTDE"].ToString());
                        nota.DataNotaFiscal = DateTime.Parse(reader["NF_DATA"].ToString(), cultura);
                        nota.ValorNotaFiscal = double.Parse(reader["NF_VAL"].ToString(), cultura);
                        lista.Add(nota);
                    }
                }

                return lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NotaFiscal ObterNota(int id)
        {
            try
            {
                NotaFiscal nota = new NotaFiscal();

                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarNotaFiscal", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    var cultura = new CultureInfo("pt-BR");
                    while (reader.Read())
                    {
                        if(int.Parse(reader["NF_ID"].ToString()) == id)
                        {
                            nota.cliente = new Cliente();
                            nota.produto = new Produto();
                            nota.fornecedor = new Fornecedor();
                            nota.IdNotaFiscal = int.Parse(reader["NF_ID"].ToString());
                            nota.cliente.IdCliente = int.Parse(reader["CLI_ID"].ToString());
                            nota.cliente.NomeCliente = reader["CLI_NOME"].ToString();
                            nota.produto.IdProduto = int.Parse(reader["PROD_ID"].ToString());
                            nota.produto.NomeProduto = reader["PROD_NOME"].ToString();
                            nota.fornecedor.IdFornecedor = int.Parse(reader["FOR_ID"].ToString());
                            nota.fornecedor.NomeFornecedor = reader["FOR_NOME"].ToString();
                            nota.QtdeProdutos = int.Parse(reader["NF_QTDE"].ToString());
                            nota.DataNotaFiscal = DateTime.Parse(reader["NF_DATA"].ToString(), cultura);
                            nota.ValorNotaFiscal = double.Parse(reader["NF_VAL"].ToString(), cultura);
                            break;
                        }
                    }
                }

                return nota;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotaFiscal> ListarNotasPorProduto(int id)
        {
            try
            {
                List<NotaFiscal> lista = new List<NotaFiscal>();

                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarNotasPorProduto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    connection.Open();

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@prod_id";
                    paramId.Value = id;
                    cmd.Parameters.Add(paramId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    var cultura = new CultureInfo("pt-BR");
                    while (reader.Read())
                    {
                        NotaFiscal nota = new NotaFiscal();
                        nota.cliente = new Cliente();
                        nota.produto = new Produto();
                        nota.fornecedor = new Fornecedor();
                        //nota.IdNotaFiscal = int.Parse(reader["NF_ID"].ToString());
                        //nota.cliente.IdCliente = int.Parse(reader["CLI_ID"].ToString());
                        //nota.cliente.NomeCliente = reader["CLI_NOME"].ToString();
                        //nota.produto.IdProduto = int.Parse(reader["PROD_ID"].ToString());
                        nota.produto.NomeProduto = reader["PROD_NOME"].ToString();
                        //nota.fornecedor.IdFornecedor = int.Parse(reader["FOR_ID"].ToString());
                        //nota.fornecedor.NomeFornecedor = reader["FOR_NOME"].ToString();
                        nota.QtdeProdutos = int.Parse(reader["NF_QTDE"].ToString());
                        nota.DataNotaFiscal = DateTime.Parse(reader["NF_DATA"].ToString(), cultura);
                        //nota.ValorNotaFiscal = double.Parse(reader["NF_VAL"].ToString(), cultura);

                        lista.Add(nota);
                        break;
                    }
                }

                return lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IncluirNota(NotaFiscal nota)
        {
            try
            {
                var lista = ListarProdutos(new Fornecedor());
                Produto produto = new Produto();
                var IdProduto = lista.FirstOrDefault(x => x.Value == nota.produto.IdProduto.ToString()).Value;
                produto = ConsultarProduto(int.Parse(IdProduto));

                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("IncluirNotaFiscal", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramCliente = new SqlParameter();
                    paramCliente.ParameterName = "@cliente_id";
                    paramCliente.Value = nota.cliente.IdCliente;
                    cmd.Parameters.Add(paramCliente);

                    SqlParameter paramProduto = new SqlParameter();
                    paramProduto.ParameterName = "@produto_id";
                    paramProduto.Value = nota.produto.IdProduto;
                    cmd.Parameters.Add(paramProduto);

                    SqlParameter paramFornecedor = new SqlParameter();
                    paramFornecedor.ParameterName = "@fornecedor_id";
                    paramFornecedor.Value = nota.fornecedor.IdFornecedor;
                    cmd.Parameters.Add(paramFornecedor);

                    SqlParameter paramQtdeProduto = new SqlParameter();
                    paramQtdeProduto.ParameterName = "@qtde_produto";
                    paramQtdeProduto.Value = nota.QtdeProdutos;
                    cmd.Parameters.Add(paramQtdeProduto);

                    SqlParameter paramDataNota = new SqlParameter();
                    paramDataNota.ParameterName = "@data_nota";
                    paramDataNota.Value = nota.DataNotaFiscal;
                    cmd.Parameters.Add(paramDataNota);

                    SqlParameter paramValorNota = new SqlParameter();
                    paramValorNota.ParameterName = "@valor_nota";
                    paramValorNota.Value = (nota.QtdeProdutos * (produto.ValorProduto));
                    cmd.Parameters.Add(paramValorNota);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirNota(NotaFiscal nota)
        {
            try
            {
                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ExcluirNotaFiscal", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramID = new SqlParameter();
                    paramID.ParameterName = "@nota_id";
                    paramID.Value = nota.IdNotaFiscal;
                    cmd.Parameters.Add(paramID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarNota(NotaFiscal nota)
        {
            try
            {
                var lista  =  ListarProdutos(new Fornecedor());
                Produto produto = new Produto();
                var IdProduto = lista.FirstOrDefault(x => x.Value == nota.produto.IdProduto.ToString()).Value;
                produto = ConsultarProduto(int.Parse(IdProduto));

                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    var valor = lista.FirstOrDefault(x => x.Value == nota.produto.IdProduto.ToString()).Value;
                    SqlCommand cmd = new SqlCommand("AtualizarNotaFiscal", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramID = new SqlParameter();
                    paramID.ParameterName = "@nota_id";
                    paramID.Value = nota.IdNotaFiscal;
                    cmd.Parameters.Add(paramID);

                    SqlParameter paramCliente = new SqlParameter();
                    paramCliente.ParameterName = "@cliente_id";
                    paramCliente.Value = nota.cliente.IdCliente;
                    cmd.Parameters.Add(paramCliente);

                    SqlParameter paramProduto = new SqlParameter();
                    paramProduto.ParameterName = "@produto_id";
                    paramProduto.Value = nota.produto.IdProduto;
                    cmd.Parameters.Add(paramProduto);

                    SqlParameter paramFornecedor = new SqlParameter();
                    paramFornecedor.ParameterName = "@fornecedor_id";
                    paramFornecedor.Value = nota.fornecedor.IdFornecedor;
                    cmd.Parameters.Add(paramFornecedor);

                    SqlParameter paramQtdeProduto = new SqlParameter();
                    paramQtdeProduto.ParameterName = "@qtde_produto";
                    paramQtdeProduto.Value = nota.QtdeProdutos;
                    cmd.Parameters.Add(paramQtdeProduto);

                    SqlParameter paramDataNota = new SqlParameter();
                    paramDataNota.ParameterName = "@data_nota";
                    paramDataNota.Value = nota.DataNotaFiscal;
                    cmd.Parameters.Add(paramDataNota);

                    SqlParameter paramValorNota = new SqlParameter();
                    paramValorNota.ParameterName = "@valor_nota";
                    paramValorNota.Value = (nota.QtdeProdutos * (produto.ValorProduto));
                    cmd.Parameters.Add(paramValorNota);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SelectListItem> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarClientes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = (int)reader["cli_id"];
                        cliente.NomeCliente = (string)reader["cli_nome"];

                        lista.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<SelectListItem> listacliente = lista.Select(x => new SelectListItem() { Text = x.NomeCliente, Value = x.IdCliente.ToString() }).ToList();

            return listacliente;
        }

        public List<SelectListItem> ListarProdutos(Fornecedor fornecedor)
        {
            List<Produto> lista = new List<Produto>();
            var cultura = new CultureInfo("pt-BR");

            try
            {
                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarProdutos", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;

                    ///SqlParameter paramFornecedor = new SqlParameter();
                    //paramFornecedor.ParameterName = "@for_id";
                    //paramFornecedor.Value = fornecedor.IdFornecedor;
                    // cmd.Parameters.Add(paramFornecedor);

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Produto produto = new Produto();
                        //produto.fornecedor = new Fornecedor();
                        produto.IdProduto = (int)reader["prod_id"];
                        //produto.fornecedor.IdFornecedor = (int)reader["for_id"];
                        produto.NomeProduto = (string)reader["prod_nome"];
                        produto.ValorProduto = double.Parse(reader["prod_val"].ToString(), cultura);

                        lista.Add(produto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<SelectListItem> listaproduto = lista.Select(x => new SelectListItem() { Text = x.NomeProduto, Value = x.IdProduto.ToString() }).ToList();

            return listaproduto;
        }

        public List<SelectListItem> ListarFornecedores()
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            try
            {
                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ListarFornecedores", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Fornecedor fornecedor = new Fornecedor();
                        fornecedor.IdFornecedor = (int)reader["for_id"];
                        fornecedor.NomeFornecedor = (string)reader["for_nome"];

                        lista.Add(fornecedor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<SelectListItem> listafornecedor = lista.Select(x => new SelectListItem() { Text = x.NomeFornecedor, Value = x.IdFornecedor.ToString() }).ToList();

            return listafornecedor;
        }

        public Produto ConsultarProduto(int id)
        {
            Produto produto = new Produto();
            var cultura = new CultureInfo("pt-BR");

            try
            {
                using (connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASA"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ConsultarProduto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@produto_id";
                    paramId.Value = id;
                    cmd.Parameters.Add(paramId);

                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        produto.IdProduto = (int)reader["prod_id"];
                        produto.NomeProduto = (string)reader["prod_nome"];
                        produto.ValorProduto = double.Parse(reader["prod_val"].ToString(), cultura);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return produto;
        }


    }
}