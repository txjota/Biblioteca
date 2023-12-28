using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.Controllers
{
    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel
            {
                Livros = livroService.ListarDisponiveis()
            };
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            EmprestimoService emprestimoService = new EmprestimoService();

            if (viewModel.Emprestimo.Devolvido && viewModel.Emprestimo.Id != 0)
            {
                emprestimoService.Excluir(viewModel.Emprestimo.Id);
            }
            else if (viewModel.Emprestimo.Id == 0)
            {
                emprestimoService.Inserir(viewModel.Emprestimo);
            }
            else
            {
                emprestimoService.Atualizar(viewModel.Emprestimo);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int pagina = 1)
        {
            Autenticacao.CheckLogin(this);
            const int tamanhoPagina = 10;
            EmprestimoService emprestimoService = new EmprestimoService();

            FiltrosEmprestimos objFiltro = null;
            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosEmprestimos
                {
                    Filtro = filtro,
                    TipoFiltro = tipoFiltro
                };
            }

            var model = emprestimoService.ListarPaginado(objFiltro, pagina, tamanhoPagina);
            return View(model);
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();
            Emprestimo emprestimo = emprestimoService.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel
            {
                Livros = livroService.ListarDisponiveis(),
                Emprestimo = emprestimo
            };

            return View(cadModel);
        }
    }
}
