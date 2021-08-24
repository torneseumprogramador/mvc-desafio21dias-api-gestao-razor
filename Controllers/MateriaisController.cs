using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc_desafio21dias_api_gestao_razor.Servico;
using web_renderizacao_server_side.Models;
using mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado;

namespace mvc_desafio21dias_api_gestao_razor.Controllers
{
    public class MateriaisController : Controller
    {
        private readonly IHttpClientService<Material> _servico;
        public MateriaisController(IHttpClientService<Material> service)
        {
            _servico = service;
        }
        public async Task<IActionResult> Index(int pagina = 1)
        {
            return View(await _servico.TodosPaginado(pagina));
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var material = await _servico.BuscaPorId(id);
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
                var mat = await _servico.Salvar(material);
                return Redirect($"/Materiais/Details/{mat.Id}");
            }
            return View(material);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _servico.BuscaPorId(id);
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
                await _servico.Salvar(material);
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _servico.BuscaPorId(id);
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
            await _servico.ExcluirPorId(id);
            return RedirectToAction(nameof(Index));
        }
    }
}