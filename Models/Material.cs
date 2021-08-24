using System.ComponentModel.DataAnnotations;
using mvc_desafio21dias_api_gestao_razor.Models;

namespace web_renderizacao_server_side.Models
{
    public class Material : Entidade
    {

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required]
        public int AlunoId { get; set; }

    }
}