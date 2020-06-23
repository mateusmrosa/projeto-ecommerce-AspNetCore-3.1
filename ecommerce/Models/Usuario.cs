using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWord.Models
{
    public class Usuario
    {
        int _id;
        string _nomeUsuario;
        int _senha;

        public int Id { get => _id; set => _id = value; }
        public string NomeUsuario { get => _nomeUsuario; set => _nomeUsuario = value; }
        public int Senha { get => _senha; set => _senha = value; }

        public Usuario()
        {
            _id = 0;
            _nomeUsuario = "";
            _senha = 0;
        }
        public Usuario(int id, string nomeUsuario, int senha)
        {
            _id = id;
            _nomeUsuario = nomeUsuario;
            _senha = senha;
        }
    }
}
