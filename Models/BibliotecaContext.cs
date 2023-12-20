using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;DataBase=Biblioteca;Uid=root;");
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
