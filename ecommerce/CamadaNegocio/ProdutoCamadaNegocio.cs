using ecommerce.CamadaAcessoDados;
using HelloWorld.Models;
using System.Collections.Generic;

namespace ecommerce.CamadaNegocio
{
    public class ProdutoCamadaNegocio
    {

        public (bool, string) Cadastrar(Produto produto)
        {
            string msg = "";
            bool operacao = false;
            ProdutoBD produtoBD = new ProdutoBD();

            if (produto.Nome.Length < 3)
            {
                msg = "Nome muito pequeno para o produto!";
            }
            else
            {
                produtoBD = new ProdutoBD();
                operacao = produtoBD.Cadastrar(produto);
            }
            return (operacao, msg);
        }

        public (bool, string) IncluirFoto(int id, byte[] foto)
        {
            string msg = "";
            bool operacao = false;
            ProdutoBD produtoBD = new ProdutoBD();

            produtoBD = new ProdutoBD();
            operacao = produtoBD.IncluirFoto(id, foto);

            return (operacao, msg = "Dados e imagem registrados com sucesso!");
        }

        public List<Produto> ObterProdutos(string produto)
        {
            ProdutoBD produtoBD = new ProdutoBD();
            return produtoBD.ObterProdutos(produto);

        }

        public byte[] ObterFoto(int id)
        {
            ProdutoBD produtoBD = new ProdutoBD();
            return produtoBD.ObterFoto(id);
        }








    }
}
