using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web_renderizacao_server_side;
using web_renderizacao_server_side.Models;

namespace mvc_desafio21dias_api_gestao_razor.Servico
{
    public class PaiServico
    {
        public static async Task<List<Pai>> Todos(int pagina = 1)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{Program.PaisAPI}/pais?page={pagina}"))
                {
                    if(!response.IsSuccessStatusCode) return new List<Pai>();

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Pai>>(json);
                }
            }
        }

        public static async Task<Pai> BuscaPorId(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{Program.PaisAPI}/pais/{id}"))
                {
                    if(!response.IsSuccessStatusCode) return null;
                    return JsonConvert.DeserializeObject<Pai>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public static async Task<Pai> Salvar(Pai pai)
        {
            using (var http = new HttpClient())
            {
                if(Convert.ToInt32(pai.Id) == 0)
                {
                    using (var response = await http.PostAsJsonAsync($"{Program.PaisAPI}/pais", pai))
                    {
                        string retorno = await response.Content.ReadAsStringAsync();
                        if(!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("========================");
                            Console.WriteLine(retorno);
                            Console.WriteLine("========================");
                            throw new Exception("Erro ao incluir na API");
                        }
                        return JsonConvert.DeserializeObject<Pai>(retorno);
                    }
                }
                else
                {
                    using (var response = await http.PutAsJsonAsync($"{Program.PaisAPI}/pais/{pai.Id}", pai))
                    {
                        if(!response.IsSuccessStatusCode) throw new Exception("Erro ao atualizar na API");
                        return JsonConvert.DeserializeObject<Pai>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task ExcluirPorId(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.DeleteAsync($"{Program.PaisAPI}/pais/{id}"))
                {
                    if(!response.IsSuccessStatusCode) throw new Exception("Erro ao excluir da API");
                }
            }
        }
    }
}