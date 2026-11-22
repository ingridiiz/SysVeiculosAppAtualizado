using Microsoft.Data.Sqlite;
using System.IO;
using Microsoft.Maui.Storage;

namespace SysVeiculosApp;

public static class BancoDados
{
    private const string NomeArquivo = "sysveiculosapp.db";

    public static string CaminhoBanco => Path.Combine(FileSystem.AppDataDirectory, NomeArquivo);

    private static bool _estruturaCriada;

    public static SqliteConnection ObterConexao()
    {
        var conexao = new SqliteConnection($"Data Source={CaminhoBanco}");
        conexao.Open();

        using var pragma = conexao.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();

        // Garante as tabelas usadas pelos repositórios (roda uma vez por execução do app,
        // mesmo que o arquivo .db já exista com estrutura antiga).
        if (!_estruturaCriada)
        {
            using var cmd = conexao.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS tblMarcas (
                    marid INTEGER PRIMARY KEY,
                    marnome TEXT NOT NULL,
                    marobservacoes TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS tblmodelos (
                    modid INTEGER PRIMARY KEY,
                    modnome TEXT NOT NULL,
                    modobservacoes TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS tblveiculos (
                    veid INTEGER PRIMARY KEY,
                    veianofabricacao INTEGER NOT NULL,
                    veianomodelo INTEGER NOT NULL,
                    veinome TEXT NOT NULL,
                    veiobservacoes TEXT NOT NULL,
                    marid INTEGER NOT NULL,
                    modid INTEGER NOT NULL,
                    FOREIGN KEY (marid) REFERENCES tblMarcas(marid),
                    FOREIGN KEY (modid) REFERENCES tblmodelos(modid)
                );
            ";
            cmd.ExecuteNonQuery();
            _estruturaCriada = true;
        }

        return conexao;
    }
}