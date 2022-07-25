using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using web_renderizacao_server_side.Models;
using Newtonsoft.Json;
using web_renderizacao_server_side;
using System.Net.Http.Headers;

namespace mvc_desafio21dias_api_gestao_razor.Servico
{
    public class AlunoServico
    {
        public static async Task<List<Aluno>> Todos(int pagina = 1, string token = "")
        {
            return (await TodosPaginado(pagina, token)).Results;
        }
        public static async Task<Paginacao<Aluno>> TodosPaginado(int pagina = 1, string token = "")
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("======[" + token + "]=======");

                using (var response = await http.GetAsync($"{Program.AlunosAPI}/alunos?page={pagina}"))
                {
                    Console.WriteLine("======[" + response.StatusCode + "]=======");
                    if(!response.IsSuccessStatusCode) return new Paginacao<Aluno>();

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Paginacao<Aluno>>(json);
                }
            }
        }

        public static async Task<Aluno> BuscaPorId(int id, string token)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await http.GetAsync($"{Program.AlunosAPI}/alunos/{id}"))
                {
                    if(!response.IsSuccessStatusCode) return null;
                    return JsonConvert.DeserializeObject<Aluno>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public static async Task<Aluno> Salvar(Aluno aluno, string token)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if(aluno.Id == 0)
                {
                    using (var response = await http.PostAsJsonAsync($"{Program.AlunosAPI}/alunos", aluno))
                    {
                        string retorno = await response.Content.ReadAsStringAsync();
                    
                        Console.WriteLine("==========[" + response.IsSuccessStatusCode + "]==============");
                        
                        if(!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("==========[" + token + "]==============");
                            Console.WriteLine(retorno);
                            Console.WriteLine("========================");
                            throw new Exception("Erro ao incluir na API");
                        }
                        return JsonConvert.DeserializeObject<Aluno>(retorno);
                    }
                }
                else
                {
                    using (var response = await http.PutAsJsonAsync($"{Program.AlunosAPI}/alunos/{aluno.Id}", aluno))
                    {
                        if(!response.IsSuccessStatusCode) throw new Exception("Erro ao atualizar na API");
                        return JsonConvert.DeserializeObject<Aluno>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task ExcluirPorId(int id, string token)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await http.DeleteAsync($"{Program.AlunosAPI}/alunos/{id}"))
                {
                    if(!response.IsSuccessStatusCode) throw new Exception("Erro ao excluir da API");
                }
            }
        }
    }
}