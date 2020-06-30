using ecommerce.Models;
using HelloWord.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.CamadaAcessoDados
{
    public class PerfilBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public Models.Perfil Obter(int id)
        {
            Models.Perfil perfil = null;

            string select = @"select * 
                              from perfil 
                              where id = " + id;

            DataTable dt = _bd.ExecutaSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                perfil = Map(dt.Rows[0]);
            }

            return perfil;

        }

        public List<Models.Perfil> Pesquisar(string nome)
        {

            List<Models.Perfil> perfis = new List<Models.Perfil>();

            string select = @"select * 
                              from Perfil 
                              where nome like @nome";

            var parametros = _bd.GerarParametros();
            parametros.Add("@nome", "%" + nome + "%");

            DataTable dt = _bd.ExecutaSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                perfis.Add(Map(row));
            }

            return perfis;

        }

        internal Models.Perfil Map(DataRow row)
        {
            Models.Perfil perfil = new Models.Perfil();
            perfil.Id = Convert.ToInt32(row["id"]);
            perfil.Nome = row["nome"].ToString();

            return perfil;
        }




    }
}
