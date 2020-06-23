using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class Produto
    {
        int _id;
        string _nome;
        string _descricao;
        int _qtde;
        double _preco;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public int Qtde { get => _qtde; set => _qtde = value; }
        public double Preco { get => _preco; set => _preco = value; }

        public Produto()
        {
            _id = 0;
            _nome = "";
            _descricao = "";
            _qtde = 0;
            _preco = 0;
        }

        public Produto(int id, string nome, string descricao, int qtde, double preco)
        {
            _id = id;
            _nome = nome;
            _descricao = descricao;
            _qtde = qtde;
            _preco = preco;
        }

      

        
    }
}
