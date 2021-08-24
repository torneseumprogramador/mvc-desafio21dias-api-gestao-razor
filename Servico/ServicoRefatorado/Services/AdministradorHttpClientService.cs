using Microsoft.Extensions.Configuration;
using web_renderizacao_server_side.Models;

namespace mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Services
{
    public class AdministradorHttpClientService : HttpClientService<Administrador>
    {
        const string SERVICE_URL = "AdministradoresAPI";
        //Se precisarmos aumentar a quantidade de parametros do contrutor do serviço
        //IConfiguration será resolvido automaticamente pela injeção de 
        public AdministradorHttpClientService(IConfiguration configuration) : base(configuration, SERVICE_URL)
        {
        }

        //Qualquer outro metodo individual do administradores referente requisção http
    }
}