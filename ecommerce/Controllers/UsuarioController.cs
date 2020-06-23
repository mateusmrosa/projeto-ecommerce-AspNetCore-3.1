using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers.Usuario
{
    [Authorize("CookieAutenticacao")]

    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            int senhaNumero;
            string msg = "";
            if (!int.TryParse(dados["senha"], out senhaNumero))
            {
                msg = "Senha inválida. Digite apenas número.";
            }
            if (dados["senha"] != dados["senhaConf"])
            {
                msg = "Senhas diferentes.";
            }
            else
            {
                HelloWord.Models.Usuario usuario = new HelloWord.Models.Usuario();
                usuario.NomeUsuario = dados["nomeUsuario"];
                usuario.Senha = Convert.ToInt32(dados["senha"]);

                CamadaNegocio.UsuarioCamadaNegocio
                    ucn = new CamadaNegocio.UsuarioCamadaNegocio();
                (operacao, msg) = ucn.Criar(usuario);
            }

            return Json(new
            {
                operacao,
                msg
            });

        }

        public IActionResult IndexListar()
        {
            return View();
        }

        public IActionResult Pesquisar(string usuario)
        {
            CamadaNegocio.UsuarioCamadaNegocio ucn = new CamadaNegocio.UsuarioCamadaNegocio();

            List<HelloWord.Models.Usuario> usuarios = ucn.Pesquisar(usuario);

            var usuariosLimpos = new List<Object>(); //pelo (objeto anonimo) conseguimos retornar para view os atributos que quiseremos 

            foreach (var u in usuarios)
            {//nesse caso nao podemos retornar para view a senha do usuario
                var usuariosLimpo = new
                {
                    id = u.Id,
                    usuario = u.NomeUsuario
                };

                usuariosLimpos.Add(usuariosLimpo);
            }

            return Json(usuariosLimpos);
        }
    }
}