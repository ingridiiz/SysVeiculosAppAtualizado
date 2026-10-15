using System.Collections.Generic;
using System.Linq;

namespace SysVeiculosApp;

public static class VeiculosRepositorio
{
    private static readonly List<Veiculo> _veiculos = new()
    {
        new Veiculo { veid = 1, veinome = "Corolla", veianofabricacao = 2022, veianomodelos = 2023 }
    };

    public static List<Veiculo> Listar()
    {
        return _veiculos.OrderBy(v => v.veinome).ToList();
    }

    public static void Inserir(Veiculo veiculo)
    {
        veiculo.veid = _veiculos.Select(v => v.veid).DefaultIfEmpty(0).Max() + 1;
        _veiculos.Add(veiculo);
    }

    public static void Alterar(Veiculo veiculo)
    {
        var existente = _veiculos.FirstOrDefault(v => v.veid == veiculo.veid);
        if (existente == null) return;

        existente.veinome = veiculo.veinome;
        existente.veianofabricacao = veiculo.veianofabricacao;
        existente.veianomodelos = veiculo.veianomodelos;
        existente.velobservacoes = veiculo.velobservacoes;
        existente.marid = veiculo.marid;
        existente.modid = veiculo.modid;
    }

    public static void Excluir(int veid)
    {
        _veiculos.RemoveAll(v => v.veid == veid);
    }
}