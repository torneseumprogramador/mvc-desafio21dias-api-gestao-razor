using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_side.Models;
using mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Interfaces;
using web_renderizacao_server_side.Helpers;

namespace mvc_desafio21dias_api_gestao_razor.Controllers
{
    [Logado]
    public class MateriaisController : Controller
    {
        private readonly IMaterialHttpClientService _servico;
        public MateriaisController(IMaterialHttpClientService service)
        {
            _servico = service;
        }
        public async Task<IActionResult> Index(int pagina = 1)
        {
            return View(await _servico.TodosPaginado(pagina, TempData["token"].ToString()));
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var material = await _servico.BuscaPorId(id, TempData["token"].ToString());
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material)
        {
            if (ModelState.IsValid)
            {
                var mat = await _servico.Salvar(material, TempData["token"].ToString());
                return Redirect($"/Materiais/Details/{mat.Id}");
            }
            return View(material);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _servico.BuscaPorId(id, TempData["token"].ToString());
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _servico.Salvar(material, TempData["token"].ToString());
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _servico.BuscaPorId(id, TempData["token"].ToString());
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _servico.ExcluirPorId(id, TempData["token"].ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}