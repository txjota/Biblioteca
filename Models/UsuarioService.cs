using System.Linq;
using System.Collections.Generic;
using Biblioteca.Models;


namespace Biblioteca.Services
{
    public class UsuarioService
    {
        public void Inserir(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(u);
                bc.SaveChanges();
                
            }
        }


        public void Atualizar(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuarios.Find(u.Id);
                usuario.Nome = u.Nome;
                usuario.Login = u.Login;
                usuario.Senha = u.Senha;

                bc.SaveChanges();
            }
        }

        public void Excluir(int id)
{
    using (BibliotecaContext bc = new BibliotecaContext())
    {
        var usuario = bc.Usuarios.Find(id);
        if (usuario != null)
        {
            bc.Usuarios.Remove(usuario);
            bc.SaveChanges();
        }
    }
}


         public Usuario ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }


        public ICollection<Usuario> ListarTodos(FiltrosUsuarios filtro = null)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Usuario> query;
                
                if(filtro != null)
                {
                    switch(filtro.TipoFiltro)
                    {
                        case "Nome":
                            query = bc.Usuarios.Where(u => u.Nome.Contains(filtro.Filtro));
                        break;

                        case "Login":
                            query = bc.Usuarios.Where(u => u.Login.Contains(filtro.Filtro));
                        break;

                        default:
                            query = bc.Usuarios;
                        break;
                    }
                }
                else
                {
                    query = bc.Usuarios;
                }
                
                return query.OrderBy(u => u.Nome).ToList();
            }
        }
        
    }
}