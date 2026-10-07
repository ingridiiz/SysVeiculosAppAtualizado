using SysVeiculosApp;

namespace SysVeiculosApp;

public partial class App : Application
{
   
    public static DatabaseHelper Database { get; private set; }

    public App(DatabaseHelper databaseHelper)
    {
        InitializeComponent();

        Database = databaseHelper;
        MainPage = new AppShell();
    }
}