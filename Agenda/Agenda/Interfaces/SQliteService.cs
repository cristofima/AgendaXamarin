using Agenda.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agenda.Interfaces
{
    public class SQliteService 
    {
        private SQLiteAsyncConnection _sqlCon;

        public SQliteService()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            _sqlCon = new SQLiteAsyncConnection(databasePath);

            this.CreateDatabaseAsync();
        }

        private void CreateDatabaseAsync()
        {
            _sqlCon.CreateTableAsync<Persona>().Wait();
        }

        #region Métodos CRUD

        public Task<List<Persona>> GetPersonasAsync()
        {
            return _sqlCon.Table<Persona>().ToListAsync();
        }

        public Task<Persona> GetPersonaAsync(int id)
        {
            return _sqlCon.Table<Persona>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SavePersonaAsync(Persona item)
        {
            if (item.Id != 0)
            {
                return _sqlCon.UpdateAsync(item);
            }
            else
            {
                return _sqlCon.InsertAsync(item);
            }
        }

        public Task<int> DeletePersonaAsync(Persona item)
        {
            return _sqlCon.DeleteAsync(item);
        }

        #endregion Métodos CRUD
    }
}
