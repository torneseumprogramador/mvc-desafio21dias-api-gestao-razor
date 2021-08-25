using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace web_renderizacao_server_side.Helpers
{
  public class LogadoAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if( string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["adm_desafio_21dias_csharp_api"]) )
      {
          filterContext.HttpContext.Response.Redirect("/login");
          return;
      }

      if (filterContext.Controller != null)
      {
         string usuarioLogado = filterContext.HttpContext.Request.Cookies["adm_desafio_21dias_csharp_api_nome"];
         ((Controller)filterContext.Controller).TempData["usuarioLogado"] = usuarioLogado;
      }

      base.OnActionExecuting(filterContext);
    }
  }
}