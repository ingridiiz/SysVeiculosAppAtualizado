using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace SysVeiculosApp;

public static class MarcasRepositorio
{
    public static List<Marca> Listar()
    {
        var lista = new List<Marca>();

        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "SELECT marid, marnome, marobservacoes FROM tblMarcas ORDER BY marid;";

        using var leitor = comando.ExecuteReader();
        while (leitor.Read())
        {
            lista.Add(new Marca
            {
                marid = leitor.GetInt32(0),
                marnome = leitor.GetString(1),
                marobservacoes = leitor.IsDBNull(2) ? string.Empty : leitor.GetString(2)
            });
        }

        return lista;
    }

    public static void Inserir(Marca marca)
    {
        using var conexao = BancoDados.ObterConexao();

        using var cmdId = conexao.CreateCommand();
        cmdId.CommandText = "SELECT IFNULL(MAX(marid), 0) + 1 FROM tblMarcas;";
        marca.marid = System.Convert.ToInt32(cmdId.ExecuteScalar());

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            INSERT INTO tblMarcas (marid, marnome, marobservacoes)
            VALUES (@marid, @marnome, @marobservacoes);";

        comando.Parameters.AddWithValue("@marid", marca.marid);
        comando.Parameters.AddWithValue("@marnome", marca.marnome ?? string.Empty);
        comando.Parameters.AddWithValue("@marobservacoes", marca.marobservacoes ?? string.Empty);
        comando.ExecuteNonQuery();
    }

    public static void Alterar(Marca marca)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            UPDATE tblMarcas
            SET marnome = @marnome,
                marobservacoes = @marobservacoes
            WHERE marid = @marid;";

        comando.Parameters.AddWithValue("@marid", marca.marid);
        comando.Parameters.AddWithValue("@marnome", marca.marnome ?? string.Empty);
        comando.Parameters.AddWithValue("@marobservacoes", marca.marobservacoes ?? string.Empty);
        comando.ExecuteNonQuery();
    }

    public static void Excluir(int marid)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "DELETE FROM tblMarcas WHERE marid = @marid;";
        comando.Parameters.AddWithValue("@marid", marid);
        comando.ExecuteNonQuery();
    }

    public static bool ExisteModeloNaMarca(int marid)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "SELECT COUNT(*) FROM tblveiculos WHERE marid = @marid;";
        comando.Parameters.AddWithValue("@marid", marid);

        var total = System.Convert.ToInt32(comando.ExecuteScalar());
        return total > 0;
    }
}