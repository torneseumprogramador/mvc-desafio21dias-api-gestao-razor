using System.ComponentModel.DataAnnotations;

namespace mvc_desafio21dias_api_gestao_razor.Models
{
    public abstract class Entidade
    {
        [Key]
        public int Id { get; set; }
    }
}