using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            //Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();

            if (u.Id == 0)
            {
                usuarioService.Inserir(u);
            }
            else
            {
                usuarioService.Atualizar(u);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Excluir(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin")
            {
                return Unauthorized(); // Ou redirecionar para outra página
            }

            UsuarioService usuarioService = new UsuarioService();
            usuarioService.Excluir(id);

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int pagina = 1)
        {
            Autenticacao.CheckLogin(this);

            bool isAdmin = HttpContext.Session.GetString("user") == "admin";
            ViewData["IsAdmin"] = isAdmin;

            FiltrosUsuarios objFiltro = null;
            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosUsuarios();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            UsuarioService usuarioService = new UsuarioService();
            return View(usuarioService.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin")
            {
                return Unauthorized(); // Ou redirecionar para outra página
            }

            UsuarioService usuarioService = new UsuarioService();
            Usuario u = usuarioService.ObterPorId(id);
            return View(u);
        }
    }
}
