using System;
using System.Collections.Generic;
using System.Data; //importar datatable (tabela em memoria)
using System.Linq;
using System.Threading.Tasks;

//provide para o mysql
using MySql.Data.MySqlClient;

namespace ecommerce.CamadaAcessoDados
{
    /// <summary>
    /// classe suporte para acesso ao mysql
    /// </summary>
    public class MySQLPersistencia
    {
        MySqlConnection _con; // faz a conexao com o banco
        MySqlCommand _cmd; // executa as instruções sql

        int _ultimoId = 0;

        public int UltimoId { get => _ultimoId; set => _ultimoId = value; }
        //...
        public MySQLPersistencia()
        {
            string strCon = System.Environment.GetEnvironmentVariable("MYSQLSTRCON");
            _con = new MySqlConnection(strCon);
            _cmd = _con.CreateCommand();
        }
        
        /// <summary>
        /// abre a conexao com o banco
        /// </summary>
        public void Abrir()
        {
            if (_con.State != System.Data.ConnectionState.Open)
            {
                _con.Open();
            }
        }

        /// <summary>
        /// fecha a conexao com banco
        /// </summary>
        public void Fechar()
        {
            _con.Close();
        }

        /// <summary>
        /// metodo criado para nao precisar fazer e instaciar o dicionario tod hora
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GerarParametros()
        {
            return new Dictionary<string, object>();
        }

        public Dictionary<string, byte[]> GerarParametrosBinarios()
        {
            return new Dictionary<string, byte[]>();
        }

        /// <summary>
        /// só executa select
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>

        public DataTable ExecutaSelect(string select, Dictionary<string, object> parametros = null)
        {
            Abrir();

            _cmd.CommandText = select;

            DataTable dt = new DataTable();

            if(parametros != null)
            {
                foreach(var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            dt.Load(_cmd.ExecuteReader());

            Fechar();

            return dt;
        }

        /// <summary>
        /// executa insert, delete, update e stored precedure
        /// </summary>
        /// <param name="sql"></param>
        
        public int ExecutarNoQuery(string sql, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();

            _cmd.CommandText = sql;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }


            int linhasAfetadas = _cmd.ExecuteNonQuery();
            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return linhasAfetadas;
        }

        /// <summary>
        /// retorna apenas uma valor em formato object
        /// </summary>
        public object ExecutarScalar(string sql, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();

            _cmd.CommandText = sql;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }


            object retorno = _cmd.ExecuteScalar();
            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return retorno;
        }
    }
}
