using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.CamadaAcessoDados;
using ecommerce.Models;
using HelloWord.Models;

namespace ecommerce.CamadaNegocio
{
    public class UsuarioCamadaNegocio
    {
        public (bool, string) Criar(Usuario usuario)
        {
            string msg = "";
            bool operacao = false;
            UsuarioBD usuarioDB = new UsuarioBD();

            if (usuarioDB.validaNomeUnico(usuario.NomeUsuario))
            {
                msg = "Nome existente. Informe outro!";
            }

            else if (usuario.Senha.ToString().Length < 6)
            {
                msg = "Senha muito pequena.";
            }
            else
            {
                UsuarioBD usuarioBD = new UsuarioBD();
                operacao = usuarioBD.Criar(usuario);
            }
            return (operacao, msg);
        }

        public Usuario Validar(string usuarioNome, string senha)
        {
            UsuarioBD usuarioBD = new UsuarioBD();
            return usuarioBD.Validar(usuarioNome, senha);
        }

        public Usuario ObterId(int id)
        {
            CamadaAcessoDados.UsuarioBD usuarioBD = new CamadaAcessoDados.UsuarioBD();
            return usuarioBD.ObterId(id);
        }

        public List<Usuario> Pesquisar(string usuario)
        {
            CamadaAcessoDados.UsuarioBD usuarioBD = new CamadaAcessoDados.UsuarioBD();
         
            return usuarioBD.Pesquisar(usuario);
            
        }

        public List<Perfil> ObterPerfis(string nome)
        {
            PerfilBD perfilBD = new PerfilBD();
            return perfilBD.Pesquisar(nome);
        }
    }
}
