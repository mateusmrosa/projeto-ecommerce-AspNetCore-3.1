using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ecommerce.CamadaNegocio;
using HelloWorld.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{

    [Authorize("CookieAutenticacao")]

    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexCadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string msg = "";

            Produto produto = new Produto();
            produto.Nome = dados["nome"];
            produto.Descricao = dados["descricao"];
            produto.Qtde = Convert.ToInt32(dados["qtde"]);
            produto.Preco = Convert.ToDouble(dados["preco"]);

            ProdutoCamadaNegocio prodCamadaNegocio = new ProdutoCamadaNegocio();
            (operacao, msg) = prodCamadaNegocio.Cadastrar(produto);


            return Json(new
            {
                id = produto.Id,
                operacao,
                msg
            });

        }
        [HttpPost]
        public IActionResult Foto()
        {
            int idArquivo = Convert.ToInt32(Request.Form["id"]);
            string nomeArquivo = Request.Form.Files[0].FileName;

            bool operacao = false;
            string msg = "";
            string extensao = Path.GetExtension(nomeArquivo);
            string[] extensoesValidas = new string[] { ".jpg", ".png" };

            if (!extensoesValidas.Contains(extensao))
            {
                msg = "Formato de imagem inválido";
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);
                byte[] arquivo = ms.ToArray();

                ProdutoCamadaNegocio prodCamadaNegocio = new ProdutoCamadaNegocio();
                (operacao, msg) = prodCamadaNegocio.IncluirFoto(idArquivo, arquivo);

            }

            return Json(new
            {
                //idArquivo,
                operacao,
                msg
            });
        }

        public IActionResult ObterProdutos(string produto)
        {
            ProdutoCamadaNegocio prodCamNeg = new ProdutoCamadaNegocio();

            List<Produto> produtos = prodCamNeg.ObterProdutos(produto);

            return Json(produtos);
        }

        public IActionResult ObterFoto(int id)
        {
            ProdutoCamadaNegocio prodCamNeg = new ProdutoCamadaNegocio();
            byte[] foto = prodCamNeg.ObterFoto(id);

            if (foto == null)
            {
                return File("~/img/semfoto.jpg", "image/jpg");
            }
            else
            {
                return File(prodCamNeg.ObterFoto(id), "image/png");
            }

        }


    }
}