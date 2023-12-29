using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthService _authService;

        public HomeController(ILogger<HomeController> logger, AuthService authService)
        {
            _logger = logger;
        _authService = authService;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string senha)
    {
        var usuario = await _authService.Autenticar(login, senha);
        if (usuario == null)
        {
            ViewData["Erro"] = "Login ou senha inválidos";
            return View();
        }

        HttpContext.Session.SetString("user", usuario.Login);
        return RedirectToAction("Index");
    
        }

         public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}