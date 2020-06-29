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

        public Perfil Obter(int id)
        {
            Perfil perfil = null;

            string select = @"select * from perfil where id = " + id;

            DataTable dt = _bd.ExecutaSelect(select);

            if (dt.Rows.Count == 1)
            {
                perfil = Map(dt.Rows[0]);
            }

            return perfil;
        }

        public List<Perfil> Pesquisar(string nome)
        {
            List<Perfil> listaPerfis = new List<Perfil>();

            string select = @"select * from perfil where nome like @nome";

            var parametros = _bd.GerarParametros();

            parametros.Add("@nome", "%" + nome + "%");

            DataTable dt = _bd.ExecutaSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                listaPerfis.Add(Map(row));
            }

            return listaPerfis;
        }

        internal Perfil Map(DataRow row)
        {
            Perfil perfil = new Perfil();
            perfil.Id = Convert.ToInt32(row["id"]);
            perfil.Nome = row["nome"].ToString();

            return perfil;
        }



    }
}
