using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ecommerce.CamadaNegocio;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ecommerce;

namespace ecommerce.Controllers
{
    #region data-notation = fornece informações adicional sobre a clasee em especifico, no caso aqui ele fala que todas requisições devem ser monitoradas pelo 'cookieatutentication'
    [Authorize("CookieAutenticacao")]//definido no arquivo starup.cs
    #endregion
    public class DefaultController : Controller
    {
        public DefaultController(AppSettings appConfig)
        {
            //obtendo a injeção de dependendia...appConfig
        }

        [AllowAnonymous] //permitir anonimo, (requisição não autentica)
        public IActionResult Index()
        {
            // teste
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logar([FromBody] Dictionary<string, string> dados)
        {
            string usuario = dados["usuario"];
            string senha = dados["senha"];

            UsuarioCamadaNegocio usuarioCamadaNegocio = new UsuarioCamadaNegocio();

            HelloWord.Models.Usuario usuarioOk = usuarioCamadaNegocio.Validar(usuario, senha);

            if (usuarioOk != null)
            {
                #region Criando as cookie de autenticação

                var usuarioClaims = new List<Claim>()
                {
                    new Claim("id", usuarioOk.Id.ToString()),
                    new Claim("usuario", usuarioOk.NomeUsuario)
                };

                var identificacao = new ClaimsIdentity(usuarioClaims, "Identificação do Usuário");
                var principal = new ClaimsPrincipal(identificacao);

                //gerar a cookie
                Microsoft.AspNetCore
                    .Authentication
                    .AuthenticationHttpContextExtensions
                    .SignInAsync(HttpContext, principal);

                #endregion


                return Json(new
                {
                    operacao = true,
                    msg = "Seja Bem-Vindo"

                });
            }
            else
            {
                return Json(new
                {
                    operacao = false,
                    msg = "Dados inválidos"
                });
            }
        }

        public IActionResult Sair()
        {
            //excluindo a cookie
            Microsoft.AspNetCore
                .Authentication
                .AuthenticationHttpContextExtensions
                .SignOutAsync(HttpContext);

            return Redirect("/Default/Index");
        }
    }
}