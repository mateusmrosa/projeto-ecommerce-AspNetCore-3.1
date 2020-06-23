using HelloWord.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.CamadaAcessoDados
{
    public class UsuarioBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public bool Criar(Usuario usuario)
        {
            string insert = @"insert into usuarios(nome, senha) values (@nome, @senha)";

            var parametros = _bd.GerarParametros();
            parametros.Add("@nome", usuario.NomeUsuario);
            parametros.Add("@senha", usuario.Senha);

            int linhasAfetadas = _bd.ExecutarNoQuery(insert, parametros);

            if (linhasAfetadas > 0)
            {
                usuario.Id = _bd.UltimoId;
            }

            return linhasAfetadas > 0;
        }

        //public bool Validar(string usuarioNome, string senha)
        //{
        //    Dictionary<string, object> parametros = new Dictionary<string, object>();
        //    parametros.Add("@nome", usuarioNome);
        //    parametros.Add("@senha", senha);

        //    string select = @"select count(*) as conta from usuarios where nome = @nome and senha = @senha";

        //    DataTable dt = _bd.ExecutaSelect(select, parametros);

        //    int conta = Convert.ToInt32(dt.Rows[0]["conta"]);

        //    if (conta == 0)
        //    {
        //        return false;
        //    }
        //    else
        //        return true;
        //}

        public Usuario Validar(string usuarioNome, string senha)
        {
            Usuario usuarioRetorno = null;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@nome", usuarioNome);
            parametros.Add("@senha", senha);

            string select = @"select id from usuarios where nome = @nome and senha = @senha";

            DataTable dt = _bd.ExecutaSelect(select, parametros);

            int qtdeLinhas = dt.Rows.Count;

            if (qtdeLinhas > 0)
            {
                int id = Convert.ToInt32(dt.Rows[0]["id"]);
                usuarioRetorno = ObterId(id);
            }

            return usuarioRetorno;
        }

        public bool validaNomeUnico(string usuarioNome)
        {
            var parametros = _bd.GerarParametros(); //funcao gera os parametros do dicionario

            parametros.Add("@nome", usuarioNome);

            string select = @"select count(*) as conta from usuarios where nome = @nome";

            DataTable dt = _bd.ExecutaSelect(select, parametros);

            int conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
            {
                return false;
            }
            else
                return true;
        }

        public Usuario ObterId(int id)
        {
            Usuario usuario = null;

            string select = @"select * from usuarios where id = " + id;

            DataTable dt = _bd.ExecutaSelect(select);

            if (dt.Rows.Count == 1)
            {
                usuario = Map(dt.Rows[0]);
            }

            return usuario;
        }

        public List<Usuario> Pesquisar(string usuario)
        {
            List<Usuario> usuarios = new List<Usuario>();

            string select = @"select * from usuarios where nome like @nome";

            var parametros = _bd.GerarParametros();

            parametros.Add("@nome", "%" + usuario + "%");

            DataTable dt = _bd.ExecutaSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                usuarios.Add(Map(row));
            }

            return usuarios;
        }

        internal Usuario Map(DataRow row)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt32(row["id"]);
            usuario.NomeUsuario = row["nome"].ToString();
            usuario.Senha = Convert.ToInt32(row["senha"]);

            return usuario;
        }



    }
}
