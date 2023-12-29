using Biblioteca.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AuthService
{
    private readonly BibliotecaContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(BibliotecaContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Usuario> Autenticar(string login, string senha)
    {
        // Aqui você deve considerar o uso de hash de senha em vez de armazenar e comparar as senhas em texto puro
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);
    }

    public async Task<bool> ExistemUsuariosRegistrados()
    {
        return await _context.Usuarios.AnyAsync();
    }

    public async Task Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
