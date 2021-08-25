using System;
using System.ComponentModel.DataAnnotations;
using mvc_desafio21dias_api_gestao_razor.Models;

namespace web_renderizacao_server_side.Models
{
    public class Administrador : Entidade
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
