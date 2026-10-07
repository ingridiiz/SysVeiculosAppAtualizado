using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysVeiculosApp
{
    public class DatabaseHelper
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            
            _database.CreateTableAsync<Marca>().Wait();
            _database.CreateTableAsync<Modelo>().Wait();
            _database.CreateTableAsync<Veiculo>().Wait();
        }

        
        public Task<int> SalvarMarcaAsync(Marca marca)
        {
            if (marca.marid != 0)
                return _database.UpdateAsync(marca);
            else
                return _database.InsertAsync(marca);
        }

        public Task<List<Marca>> GetMarcasAsync()
        {
            return _database.Table<Marca>().ToListAsync();
        }

        public Task<int> DeletarMarcaAsync(Marca marca)
        {
            return _database.DeleteAsync(marca);
        }

 
        public Task<int> SalvarModeloAsync(Modelo modelo)
        {
            if (modelo.modid != 0)
                return _database.UpdateAsync(modelo);
            else
                return _database.InsertAsync(modelo);
        }

        public Task<List<Modelo>> GetModelosAsync()
        {
            return _database.Table<Modelo>().ToListAsync();
        }

        public Task<int> DeletarModeloAsync(Modelo modelo)
        {
            return _database.DeleteAsync(modelo);
        }

   
        public Task<int> SalvarVeiculoAsync(Veiculo veiculo)
        {
            if (veiculo.veid != 0)
                return _database.UpdateAsync(veiculo);
            else
                return _database.InsertAsync(veiculo);
        }

        public Task<List<Veiculo>> GetVeiculosAsync()
        {
            return _database.Table<Veiculo>().ToListAsync();
        }

        public Task<int> DeletarVeiculoAsync(Veiculo veiculo)
        {
            return _database.DeleteAsync(veiculo);
        }
    }
}
