using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AuthService
{
    private readonly BibliotecaContext _context;

    public AuthService(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<Usuario> Autenticar(string login, string senha)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);
    }
}
