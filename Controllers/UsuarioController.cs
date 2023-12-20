using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Services;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            var usuarios = _usuarioService.ListarTodos();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserir(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.Inserir(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Atualizar(int id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Atualizar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.Atualizar(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Delete(int id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _usuarioService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
