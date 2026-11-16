using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace SysVeiculosApp;

public static class ModelosRepositorio
{
    public static List<Modelo> Listar()
    {
        var lista = new List<Modelo>();

        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "SELECT modid, modnome, modobservacoes FROM tblmodelos ORDER BY modnome;";

        using var leitor = comando.ExecuteReader();
        while (leitor.Read())
        {
            lista.Add(new Modelo
            {
                modid = leitor.GetInt32(0),
                modnome = leitor.GetString(1),
                modobservacoes = leitor.IsDBNull(2) ? string.Empty : leitor.GetString(2)
            });
        }

        return lista;
    }

    public static void Inserir(Modelo modelo)
    {
        using var conexao = BancoDados.ObterConexao();

        using var cmdId = conexao.CreateCommand();
        cmdId.CommandText = "SELECT IFNULL(MAX(modid), 0) + 1 FROM tblmodelos;";
        modelo.modid = System.Convert.ToInt32(cmdId.ExecuteScalar());

        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            INSERT INTO tblmodelos (modid, modnome, modobservacoes)
            VALUES (@modid, @modnome, @modobservacoes);";

        comando.Parameters.AddWithValue("@modid", modelo.modid);
        comando.Parameters.AddWithValue("@modnome", modelo.modnome ?? string.Empty);
        comando.Parameters.AddWithValue("@modobservacoes", modelo.modobservacoes ?? string.Empty);
        comando.ExecuteNonQuery();
    }

    public static void Alterar(Modelo modelo)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            UPDATE tblmodelos
            SET modnome = @modnome,
                modobservacoes = @modobservacoes
            WHERE modid = @modid;";

        comando.Parameters.AddWithValue("@modid", modelo.modid);
        comando.Parameters.AddWithValue("@modnome", modelo.modnome ?? string.Empty);
        comando.Parameters.AddWithValue("@modobservacoes", modelo.modobservacoes ?? string.Empty);
        comando.ExecuteNonQuery();
    }

    public static void Excluir(int modid)
    {
        using var conexao = BancoDados.ObterConexao();
        using var comando = conexao.CreateCommand();
        comando.CommandText = "DELETE FROM tblmodelos WHERE modid = @modid;";
        comando.Parameters.AddWithValue("@modid", modid);
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