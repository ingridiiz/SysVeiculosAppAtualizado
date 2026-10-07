using SQLite;

namespace SysVeiculosApp
{
    [Table("tblMarcas")]
    public class Marca
    {
        [PrimaryKey, AutoIncrement]
        public int marid { get; set; }
        public string marnome { get; set; }
        public string marobservacoes { get; set; }
    }

    [Table("tblmodelos")]
    public class Modelo
    {
        [PrimaryKey, AutoIncrement]
        public int modid { get; set; }
        public string modnome { get; set; }
        public string modobservacoes { get; set; }
        public string marid { get; set; }
    }

    [Table("tblveiculos")]
    public class Veiculo
    {
        [PrimaryKey, AutoIncrement]
        public int veid { get; set; }
        public int veianofabricacao { get; set; }
        public int veianomodelo { get; set; }
        public string veinome { get; set; }
        public string veiobservacoes { get; set; }
        public int marid { get; set; }
        public int modid { get; set; }
    }
}
