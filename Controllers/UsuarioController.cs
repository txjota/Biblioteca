using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
          
        Autenticacao.CheckLogin(this);
         return View();   
        }
        
    }
}   