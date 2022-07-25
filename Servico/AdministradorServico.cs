
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web_renderizacao_server_side.Models;

namespace web_renderizacao_server_side.Servicos
{
    public class AdministradorServico
    {
        public static async Task<List<Administrador>> Todos(int pagina = 1, string token = "")
        {
            return (await TodosPaginado(pagina, token)).Results;
        }
        public static async Task<Paginacao<Administrador>> TodosPaginado(int pagina = 1, string token = "")
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await http.GetAsync($"{Program.AdministradoresAPI}/administradores?page={pagina}"))
                {
                    if(!response.IsSuccessStatusCode) return new Paginacao<Administrador>();

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Paginacao<Administrador>>(json);
                }
            }
        }

        public static async Task<Administrador> Logar(string email, string senha)
        {
            using (var http = new HttpClient())
            {
                var adm = new Administrador() { Email = email, Senha = senha };
                using (var response = await http.PostAsJsonAsync($"{Program.AdministradoresAPI}/administradores/login", adm))
                {
                    if(!response.IsSuccessStatusCode) return null;
                    return JsonConvert.DeserializeObject<Administrador>(await response.Content.ReadAsStringAsync());
                }
            }
        }   

        public static async Task<Administrador> BuscaPorId(int id, string token)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await http.GetAsync($"{Program.AdministradoresAPI}/administradores/{id}"))
                {
                    if(!response.IsSuccessStatusCode) return null;
                    return JsonConvert.DeserializeObject<Administrador>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public static async Task<Administrador> Salvar(Administrador administrador, string token)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if(administrador.Id == 0)
                {
                    using (var response = await http.PostAsJsonAsync($"{Program.AdministradoresAPI}/administradores", administrador))
                    {
                        string retorno = await response.Content.ReadAsStringAsync();
                        if(!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("========================");
                            Console.WriteLine(retorno);
                            Console.WriteLine("========================");
                            throw new Exception("Erro ao incluir na API");
                        }
                        return JsonConvert.DeserializeObject<Administrador>(retorno);
                    }
                }
                else
                {
                    using (var response = await http.PutAsJsonAsync($"{Program.AdministradoresAPI}/administradores/{administrador.Id}", administrador))
                    {
                        if(!response.IsSuccessStatusCode) throw new Exception("Erro ao atualizar na API");
                        return JsonConvert.DeserializeObject<Administrador>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task ExcluirPorId(int id, string token)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await http.DeleteAsync($"{Program.AdministradoresAPI}/administradores/{id}"))
                {
                    if(!response.IsSuccessStatusCode) throw new Exception("Erro ao excluir da API");
                }
            }
        }
    }
}