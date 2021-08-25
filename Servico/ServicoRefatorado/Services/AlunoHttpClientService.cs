using Microsoft.Extensions.Configuration;
using mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Interfaces;
using web_renderizacao_server_side.Models;

namespace mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Services
{
    public class AlunoHttpClientService : HttpClientService<Aluno> , IAlunoHttpClientService
    {
        const string SERVICE_URL = "AlunosAPI";
        public AlunoHttpClientService(IConfiguration configuration) : base(configuration, SERVICE_URL)
        {
        }
    }
}