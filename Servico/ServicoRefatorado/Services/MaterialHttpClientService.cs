using Microsoft.Extensions.Configuration;
using web_renderizacao_server_side.Models;

namespace mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Services
{
    public class MaterialHttpClientService : HttpClientService<Material>
    {
        const string SERVICE_URL = "MateriaisAPI";
        public MaterialHttpClientService(IConfiguration configuration) : base(configuration, SERVICE_URL)
        {
        }
    }
}