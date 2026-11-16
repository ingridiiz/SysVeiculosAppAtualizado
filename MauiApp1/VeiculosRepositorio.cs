using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace SysVeiculosApp;

public static class VeiculosRepositorio
{
    public static List<Veiculo> Listar()
    {
        var lista = new List<Veiculo>();

        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            SELECT veid, veianofabricacao, veianomodelo, veinome, veiobservacoes, marid, modid
            FROM tblveiculos
            ORDER BY veinome;";

        using var leitor = comando.ExecuteReader();
        while (leitor.Read())
        {
            lista.Add(new Veiculo
            {
                veid = leitor.GetInt32(0),
                veianofabricacao = leitor.GetInt32(1),
                veianomodelos = leitor.GetInt32(2),
                veinome = leitor.GetString(3),
                velobservacoes = leitor.IsDBNull(4) ? string.Empty : leitor.GetString(4),
                marid = leitor.GetInt32(5),
                modid = leitor.GetInt32(6)
            });
        }

        return lista;
    }

    public static void Inserir(Veiculo veiculo)
    {
        using var conexao = BancoDados.ObterConexao();

        using var cmdId = conexao.CreateCommand();
        cmdId.CommandText = "SELECT IFNULL(MAX(veid), 0) + 1 FROM tblveiculos;";
        veiculo.veid = System.Convert.ToInt32(cmdId.ExecuteScalar());

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            INSERT INTO tblveiculos (veid, veianofabricacao, veianomodelo, veinome, veiobservacoes, marid, modid)
            VALUES (@veid, @veianofabricacao, @veianomodelo, @veinome, @veiobservacoes, @marid, @modid);";

        comando.Parameters.AddWithValue("@veid", veiculo.veid);
        comando.Parameters.AddWithValue("@veianofabricacao", veiculo.veianofabricacao);
        comando.Parameters.AddWithValue("@veianomodelo", veiculo.veianomodelos);
        comando.Parameters.AddWithValue("@veinome", veiculo.veinome ?? string.Empty);
        comando.Parameters.AddWithValue("@veiobservacoes", veiculo.velobservacoes ?? string.Empty);
        comando.Parameters.AddWithValue("@marid", veiculo.marid);
        comando.Parameters.AddWithValue("@modid", veiculo.modid);
        comando.ExecuteNonQuery();
    }

    public static void Alterar(Veiculo veiculo)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            UPDATE tblveiculos
            SET veianofabricacao = @veianofabricacao,
                veianomodelo = @veianomodelo,
                veinome = @veinome,
                veiobservacoes = @veiobservacoes,
                marid = @marid,
                modid = @modid
            WHERE veid = @veid;";

        comando.Parameters.AddWithValue("@veid", veiculo.veid);
        comando.Parameters.AddWithValue("@veianofabricacao", veiculo.veianofabricacao);
        comando.Parameters.AddWithValue("@veianomodelo", veiculo.veianomodelos);
        comando.Parameters.AddWithValue("@veinome", veiculo.veinome ?? string.Empty);
        comando.Parameters.AddWithValue("@veiobservacoes", veiculo.velobservacoes ?? string.Empty);
        comando.Parameters.AddWithValue("@marid", veiculo.marid);
        comando.Parameters.AddWithValue("@modid", veiculo.modid);
        comando.ExecuteNonQuery();
    }

    public static void Excluir(int veid)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "DELETE FROM tblveiculos WHERE veid = @veid;";
        comando.Parameters.AddWithValue("@veid", veid);
        comando.ExecuteNonQuery();
    }

    public static bool ExisteVeiculoNoModelo(int modid)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "SELECT COUNT(*) FROM tblveiculos WHERE modid = @modid;";
        comando.Parameters.AddWithValue("@modid", modid);

        var total = System.Convert.ToInt32(comando.ExecuteScalar());
        return total > 0;
    }
}