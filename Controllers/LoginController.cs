using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_renderizacao_server_side.Helpers;
using web_renderizacao_server_side.Servicos;

namespace web_renderizacao_server_side.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logar(string email, string senha, string lembrar)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ViewBag.erro = "Digite o e-mail e a senha";
            }
            else
            {
                var adm = await AdministradorServico.Logar(email, senha);
                if(adm != null)
                {
                    var expira = DateTimeOffset.UtcNow.AddHours(3);
                    if(!string.IsNullOrEmpty(lembrar)) expira = DateTimeOffset.UtcNow.AddYears(1);
                    this.HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api", adm.Id.ToString(), new CookieOptions
                    {
                        Expires = expira,
                        HttpOnly = true,
                    });

                    this.HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api_nome", adm.Nome.ToString(), new CookieOptions
                    {
                        Expires = expira,
                        HttpOnly = true,
                    });

                    Response.Redirect("/");
                }
                else
                {
                    ViewBag.erro = "Usuário ou senha inválidos";
                }

            }
            return View("Index");
        }

        public IActionResult Sair()
        {
            this.HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1),
                HttpOnly = true,
            });

            this.HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api_nome", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1),
                HttpOnly = true,
            });
            return Redirect("/");
        }
    }
}
