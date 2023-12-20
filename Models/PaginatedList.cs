using System;
using System.Collections.Generic;
using System.Linq;

public class PaginatedList<T> : List<T>
{
    public int PaginaAtual { get; private set; }
    public int TotalPaginas { get; private set; }

    public PaginatedList(List<T> items, int count, int pagina, int tamanhoPagina)
    {
        PaginaAtual = pagina;
        TotalPaginas = (int)Math.Ceiling(count / (double)tamanhoPagina);

        this.AddRange(items);
    }

    public static PaginatedList<T> Create(IQueryable<T> source, int pagina, int tamanhoPagina)
    {
        var count = source.Count();
        var items = source.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();
        return new PaginatedList<T>(items, count, pagina, tamanhoPagina);
    }
}
