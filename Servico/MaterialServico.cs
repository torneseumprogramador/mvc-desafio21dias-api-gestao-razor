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
    public class MaterialServico
    {
        public static async Task<List<Material>> Todos(int pagina = 1)
        {
            return (await TodosPaginado(pagina)).Results;
        }
        public static async Task<Paginacao<Material>> TodosPaginado(int pagina = 1)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{Program.MateriaisAPI}/materiais?page={pagina}"))
                {
                    if(!response.IsSuccessStatusCode) return new Paginacao<Material>();

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Paginacao<Material>>(json);
                }
            }
        }

        public static async Task<Material> BuscaPorId(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{Program.MateriaisAPI}/materiais/{id}"))
                {
                    if(!response.IsSuccessStatusCode) return null;
                    return JsonConvert.DeserializeObject<Material>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public static async Task<Material> Salvar(Material material)
        {
            using (var http = new HttpClient())
            {
                if(material.Id == 0)
                {
                    using (var response = await http.PostAsJsonAsync($"{Program.MateriaisAPI}/materiais", material))
                    {
                        string retorno = await response.Content.ReadAsStringAsync();
                        if(!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("========================");
                            Console.WriteLine(retorno);
                            Console.WriteLine("========================");
                            throw new Exception("Erro ao incluir na API");
                        }
                        return JsonConvert.DeserializeObject<Material>(retorno);
                    }
                }
                else
                {
                    using (var response = await http.PutAsJsonAsync($"{Program.MateriaisAPI}/materiais/{material.Id}", material))
                    {
                        if(!response.IsSuccessStatusCode) throw new Exception("Erro ao atualizar na API");
                        return JsonConvert.DeserializeObject<Material>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task ExcluirPorId(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.DeleteAsync($"{Program.MateriaisAPI}/materiais/{id}"))
                {
                    if(!response.IsSuccessStatusCode) throw new Exception("Erro ao excluir da API");
                }
            }
        }
    }
}