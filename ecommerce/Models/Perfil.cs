using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class Perfil
    {
        int _id;
        string _nome;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }

        public Perfil()
        {

        }
    }
}
