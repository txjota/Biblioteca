using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

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
            if (emprestimo != null)
            {
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                bc.SaveChanges();
            }
        }
    }

    public void Excluir(int id)
    {
        using(BibliotecaContext bc = new BibliotecaContext())
        {
            var emprestimo = bc.Emprestimos.Include(e => e.Livro).FirstOrDefault(e => e.Id == id);
            if (emprestimo != null)
            {
                // Implementar a lógica para atualizar o status do livro para disponível
                // emprestimo.Livro.Disponivel = true;
                
                bc.Emprestimos.Remove(emprestimo);
                bc.SaveChanges();
            }
        }
    }

    public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
    {
        using (BibliotecaContext bc = new BibliotecaContext())
        {
            IQueryable<Emprestimo> query = bc.Emprestimos.AsQueryable();

            if (filtro != null)
            {
                // Aplicar os filtros aqui conforme necessário
            }

            return query.Include(e => e.Livro).OrderByDescending(e => e.DataEmprestimo).ToList();
        }
    }

    public Emprestimo ObterPorId(int id)
    {
        using(BibliotecaContext bc = new BibliotecaContext())
        {
            return bc.Emprestimos.Include(e => e.Livro).FirstOrDefault(e => e.Id == id);
        }
    }

    public PaginatedList<Emprestimo> ListarPaginado(FiltrosEmprestimos filtro, int pagina, int tamanhoPagina)
    {
        using (BibliotecaContext bc = new BibliotecaContext())
        {
            IQueryable<Emprestimo> query = bc.Emprestimos.Include(e => e.Livro);

            if (filtro != null)
            {
                // Aplicar os filtros aqui conforme necessário
            }

            var totalEmprestimos = query.Count();
            var emprestimosPaginados = query.OrderByDescending(e => e.DataEmprestimo)
                                             .Skip((pagina - 1) * tamanhoPagina)
                                             .Take(tamanhoPagina)
                                             .ToList();

            return new PaginatedList<Emprestimo>(emprestimosPaginados, totalEmprestimos, pagina, tamanhoPagina);
        }
    }
}
