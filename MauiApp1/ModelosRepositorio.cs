using System.Collections.Generic;
using System.Linq;

namespace SysVeiculosApp;

public static class ModelosRepositorio
{
    private static readonly List<Modelo> _modelos = new()
    {
        new Modelo { modid = 1, modnome = "Civic", marid = 2 },   
        new Modelo { modid = 2, modnome = "Corolla", marid = 1 }  
    };

    public static List<Modelo> Listar()
    {
        return _modelos.OrderBy(m => m.modnome).ToList();
    }

    public static void Inserir(Modelo modelo)
    {
        modelo.modid = _modelos.Select(m => m.modid).DefaultIfEmpty(0).Max() + 1;
        _modelos.Add(modelo);
    }

    public static void Alterar(Modelo modelo)
    {
        var existente = _modelos.FirstOrDefault(m => m.modid == modelo.modid);
        if (existente == null) return;

        existente.modnome = modelo.modnome;
        existente.modobservacoes = modelo.modobservacoes;
        existente.marid = modelo.marid;
    }

    public static void Excluir(int modid)
    {
        _modelos.RemoveAll(m => m.modid == modid);
    }


    public static bool ExisteVeiculoNoModelo(int modid)
    {
        return VeiculosRepositorio.Listar().Any(v => v.modid == modid);
    }
}