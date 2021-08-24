using System;
using System.ComponentModel.DataAnnotations;

namespace web_renderizacao_server_side.Models
{
    public class Administrador
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
