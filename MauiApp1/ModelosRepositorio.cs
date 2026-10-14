namespace SysVeiculosApp;

public static class ModelosRepositorio
{
    private static readonly List<Modelo> _modelos = new()
    {
        new Modelo { modid = 1, modnome = "Civic" },
        new Modelo { modid = 2, modnome = "Corolla" }
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
    }

    public static void Excluir(int modid)
    {
        _modelos.RemoveAll(m => m.modid == modid);
    }
}