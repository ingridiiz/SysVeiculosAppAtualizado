using System.Collections.Generic;
using System.Linq;

namespace SysVeiculosApp;

public static class MarcasRepositorio
{
    private static readonly List<Marca> _marcas = new()
    {
        new Marca { marid = 1, marnome = "Toyota" },
        new Marca { marid = 2, marnome = "Honda" }
    };

    public static List<Marca> Listar()
    {
        return _marcas.OrderBy(m => m.marnome).ToList();
    }

    public static void Inserir(Marca marca)
    {
        marca.marid = _marcas.Select(m => m.marid).DefaultIfEmpty(0).Max() + 1;
        _marcas.Add(marca);
    }

    public static void Alterar(Marca marca)
    {
        var existente = _marcas.FirstOrDefault(m => m.marid == marca.marid);
        if (existente == null) return;

        existente.marnome = marca.marnome;
        existente.marobservacoes = marca.marobservacoes;
    }

    public static void Excluir(int marid)
    {
        _marcas.RemoveAll(m => m.marid == marid);
    }
}