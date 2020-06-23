using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.CamadaAcessoDados
{
    public class ProdutoBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public bool Cadastrar(Produto produto)
        {
            string insert = @"insert into produto(nome, qtde, descricao, preco) 
                                values (@nome, @qtde, @descricao, @preco)";

            var parametros = _bd.GerarParametros();

            parametros.Add("@nome", produto.Nome);
            parametros.Add("@qtde", produto.Qtde);
            parametros.Add("@descricao", produto.Descricao);
            parametros.Add("@preco", produto.Preco);

            int linhasAfetadas = _bd.ExecutarNoQuery(insert, parametros);

            if (linhasAfetadas > 0)
            {
                produto.Id = _bd.UltimoId;
            }

            return linhasAfetadas > 0;
        }

        public bool IncluirFoto(int id, byte[] foto)
        {
            string insert = @"update produto set foto = @foto where id = @id";

            var parametros = _bd.GerarParametros();
            parametros.Add("@id", id);

            var parametrosBinarios = _bd.GerarParametrosBinarios();
            parametrosBinarios.Add("@foto", foto);

            int linhasAfetadas = _bd.ExecutarNoQuery(insert, parametros, parametrosBinarios);

            return linhasAfetadas > 0;
        }

        public List<Produto> ObterProdutos(string produto)
        {
            List<Produto> produtos = new List<Produto>();

            string select = @"select * from produto where nome like @nome";

            var parametros = _bd.GerarParametros();

            parametros.Add("@nome", "%" + produto + "%");

            DataTable dt = _bd.ExecutaSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                produtos.Add(Map(row));
            }

            return produtos;
        }

        public byte[] ObterFoto(int id)
        {
            byte[] retorno = null;
            string select = @"select foto from produto where id = " + id;

            object fotoDB = _bd.ExecutarScalar(select);

            if (fotoDB != DBNull.Value)
            {
                retorno = (byte[])fotoDB;
            }
            return retorno;
        }

        internal Produto Map(DataRow row)
        {
            Produto produto = new Produto();
            produto.Id = Convert.ToInt32(row["id"]);
            produto.Nome = row["nome"].ToString();
            produto.Descricao = row["descricao"].ToString();
            produto.Preco = Convert.ToDouble(row["preco"]);

            return produto;
        }



    }
}
