using Microsoft.Extensions.Configuration;
using web_renderizacao_server_side.Models;

namespace mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Services
{
    public class PaiHttpClientService : HttpClientService<Pai>
    {
        const string SERVICE_URL = "PaisAPI";
        public PaiHttpClientService(IConfiguration configuration) : base(configuration, SERVICE_URL)
        {
        }
    }
}