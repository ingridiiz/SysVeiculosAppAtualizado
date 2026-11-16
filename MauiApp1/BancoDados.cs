using Microsoft.Data.Sqlite;
using System.IO;
using Microsoft.Maui.Storage;

namespace SysVeiculosApp;

public static class BancoDados
{
    private const string NomeArquivo = "sysveiculosapp.db";

    public static string CaminhoBanco => Path.Combine(FileSystem.AppDataDirectory, NomeArquivo);

    public static SqliteConnection ObterConexao()
    {
        if (!File.Exists(CaminhoBanco))
        {
            using var streamOrigem = FileSystem.OpenAppPackageFileAsync(NomeArquivo).GetAwaiter().GetResult();
            using var streamDestino = File.Create(CaminhoBanco);
            streamOrigem.CopyTo(streamDestino);
        }

        var conexao = new SqliteConnection($"Data Source={CaminhoBanco}");
        conexao.Open();

        using var pragma = conexao.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();

        return conexao;
    }
}