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
            var conexaoCriacao = new SqliteConnection($"Data Source={CaminhoBanco}");
            conexaoCriacao.Open();

            using var cmd = conexaoCriacao.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Marca (
                    marid INTEGER PRIMARY KEY AUTOINCREMENT,
                    marnome TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Modelo (
                    modid INTEGER PRIMARY KEY AUTOINCREMENT,
                    modnome TEXT NOT NULL,
                    marid INTEGER NOT NULL,
                    FOREIGN KEY(marid) REFERENCES Marca(marid)
                );
                CREATE TABLE IF NOT EXISTS Veiculo (
                    veiid INTEGER PRIMARY KEY AUTOINCREMENT,
                    veinome TEXT NOT NULL,
                    veianofabricacao INTEGER,
                    veianomodelos INTEGER,
                    velobservacoes TEXT,
                    modid INTEGER NOT NULL,
                    FOREIGN KEY(modid) REFERENCES Modelo(modid)
                );
            ";
            cmd.ExecuteNonQuery();
        }

        var conexao = new SqliteConnection($"Data Source={CaminhoBanco}");
        conexao.Open();

        using var pragma = conexao.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();

        return conexao;
    }
}