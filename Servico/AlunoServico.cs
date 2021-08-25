using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using web_renderizacao_server_side.Models;
using Newtonsoft.Json;
using web_renderizacao_server_side;


namespace mvc_desafio21dias_api_gestao_razor.Servico
{
    public class AlunoServico
    {
        public static async Task<List<Aluno>> Todos(int pagina = 1)
        {
            return (await TodosPaginado(pagina)).Results;
        }
        public static async Task<Paginacao<Aluno>> TodosPaginado(int pagina = 1)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{Program.AlunosAPI}/alunos?page={pagina}"))
                {
                    if(!response.IsSuccessStatusCode) return new Paginacao<Aluno>();

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Paginacao<Aluno>>(json);
                }
            }
        }

        public static async Task<Aluno> BuscaPorId(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{Program.AlunosAPI}/alunos/{id}"))
                {
                    if(!response.IsSuccessStatusCode) return null;
                    return JsonConvert.DeserializeObject<Aluno>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public static async Task<Aluno> Salvar(Aluno aluno)
        {
            using (var http = new HttpClient())
            {
                if(aluno.Id == 0)
                {
                    using (var response = await http.PostAsJsonAsync($"{Program.AlunosAPI}/alunos", aluno))
                    {
                        string retorno = await response.Content.ReadAsStringAsync();
                        if(!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("========================");
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

        public static async Task ExcluirPorId(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.DeleteAsync($"{Program.AlunosAPI}/alunos/{id}"))
                {
                    if(!response.IsSuccessStatusCode) throw new Exception("Erro ao excluir da API");
                }
            }
        }
    }
}