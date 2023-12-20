using Biblioteca.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class UsuarioService
{
    private readonly BibliotecaContext _context;

    public UsuarioService(BibliotecaContext context)
    {
        _context = context;
    }

    public List<Usuario> ListarTodos()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario ObterPorId(int id)
    {
        return _context.Usuarios.Find(id);
    }

    public void Inserir(Usuario usuario)
    {
        usuario.Senha = HashMD5(usuario.Senha);
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public void Atualizar(Usuario usuario)
    {
        var usuarioExistente = _context.Usuarios.Find(usuario.Id);
        if (usuarioExistente != null)
        {
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.DataNascimento = usuario.DataNascimento;
            // A senha só é atualizada se uma nova for fornecida
            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuarioExistente.Senha = HashMD5(usuario.Senha);
            }

            _context.SaveChanges();
        }
    }

    public void Excluir(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }

    // Método para criar hash de senha com MD5
    private string HashMD5(string password)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
