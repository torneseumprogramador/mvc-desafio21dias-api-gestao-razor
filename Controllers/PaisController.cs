using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc_desafio21dias_api_gestao_razor.Servico;
using web_renderizacao_server_side.Models;
using web_renderizacao_server_side.Helpers;

namespace mvc_desafio21dias_api_gestao_razor.Controllers
{
    [Logado]
    public class PaisController : Controller
    {
        public async Task<IActionResult> Index(int pagina = 1)
        {
            return View(await PaiServico.Todos(pagina, TempData["token"].ToString()));
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pai = await PaiServico.BuscaPorId(id, TempData["token"].ToString());
            if (pai == null)
            {
                return NotFound();
            }

            return View(pai);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pai pai)
        {
            if (ModelState.IsValid)
            {
                var p = await PaiServico.Salvar(pai, TempData["token"].ToString());
                return Redirect($"/Pais/Details/{p.Id}");
            }
            return View(pai);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pai = await PaiServico.BuscaPorId(id, TempData["token"].ToString());
            if (pai == null)
            {
                return NotFound();
            }
            return View(pai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Pai pai)
        {
            if (id != pai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PaiServico.Salvar(pai, TempData["token"].ToString());
                return RedirectToAction(nameof(Index));
            }
            return View(pai);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pai = await PaiServico.BuscaPorId(id, TempData["token"].ToString());
            if (pai == null)
            {
                return NotFound();
            }

            return View(pai);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await PaiServico.ExcluirPorId(id, TempData["token"].ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}