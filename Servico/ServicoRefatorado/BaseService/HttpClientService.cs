using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web_renderizacao_server_side.Models;
using System;
using System.Net.Http.Json;
using mvc_desafio21dias_api_gestao_razor.Models;
using Microsoft.Extensions.Configuration;

namespace mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado
{
    public abstract class HttpClientService<T> : IHttpClientService<T> where T : Entidade
    {
        private readonly HttpClient _http;
        private readonly string _urlServico;

        protected HttpClientService(IConfiguration configuration, string urlServico)
        {
            _http = new HttpClient();
            _urlServico = configuration.GetConnectionString(urlServico);
        }

        public async Task<T> BuscaPorId(int id)
        {
            using (var response = await _http.GetAsync($"{_urlServico}/{id}"))
            {
                if(!response.IsSuccessStatusCode) return null;
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        public void Dispose()
        {
            _http?.Dispose();
        }

        public async Task ExcluirPorId(int id)
        {
            using (var response = await _http.DeleteAsync($"{_urlServico}/{id}"))
            {
                if(!response.IsSuccessStatusCode) throw new Exception("Erro ao excluir da API");
            }
        }

        public async Task<T> Salvar(T entidade)
        {
            if(entidade.Id == 0)
                {
                    using (var response = await _http.PostAsJsonAsync($"{_urlServico}", entidade))
                    {
                        string retorno = await response.Content.ReadAsStringAsync();
                        if(!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("========================");
                            Console.WriteLine(retorno);
                            Console.WriteLine("========================");
                            throw new Exception("Erro ao incluir na API");
                        }
                        return JsonConvert.DeserializeObject<T>(retorno);
                    }
                }
                else
                {
                    using (var response = await _http.PutAsJsonAsync($"{_urlServico}/{entidade.Id}", entidade))
                    {
                        if(!response.IsSuccessStatusCode) throw new Exception("Erro ao atualizar na API");
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    }
                }
        }

        public virtual async Task<List<T>> Todos(int pagina = 1)
        {
            return (await TodosPaginado(pagina)).Results;
        }

        public virtual async Task<Paginacao<T>> TodosPaginado(int pagina = 1)
        {
            
            using (var response = await _http.GetAsync($"{_urlServico}?page={pagina}"))
            {
                if(!response.IsSuccessStatusCode) return new Paginacao<T>();

                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Paginacao<T>>(json);
            }
            
        }
    }
}