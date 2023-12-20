using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;

                bc.SaveChanges();
            }
        }


        public void Excluir(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                var emprestimo = bc.Emprestimos.Include(e => e.Livro).FirstOrDefault(e => e.Id == id);
                if (emprestimo != null)
                {
                    //implementar depois
                    //emprestimo.Livro.Disponivel = true;

                    bc.Emprestimos.Remove(emprestimo);
                    bc.SaveChanges();
                }
            }
        }





      public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
{
    using (BibliotecaContext bc = new BibliotecaContext())
    {
        var query = bc.Emprestimos.AsQueryable();

        if (filtro != null)
        {
            if (!string.IsNullOrEmpty(filtro.TipoFiltro) && !string.IsNullOrEmpty(filtro.Filtro))
            {
                switch (filtro.TipoFiltro.ToLower())
                {
                    case "usuario":
                        query = query.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                        break;
                    case "livro":
                        if (int.TryParse(filtro.Filtro, out int livroId))
                        {
                            query = query.Where(e => e.LivroId == livroId);
                        }
                        break;
                        
                }
            }
        }

        query = query.OrderByDescending(e => e.DataDevolucao);
        
        return query.Include(e => e.Livro).ToList();
    }
}






        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}