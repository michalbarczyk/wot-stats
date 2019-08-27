
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WoTStats.Models.DatabaseModels;

namespace WoTStats.Services
{
    public class UserDatabase
    {
        private readonly SQLiteAsyncConnection databaseAsyncConnection;
        private readonly SQLiteConnection databaseConnection;
       

        public UserDatabase(string dbPath)
        {
            databaseAsyncConnection = new SQLiteAsyncConnection(dbPath);
            databaseConnection = new SQLiteConnection(dbPath);

            databaseAsyncConnection.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return databaseAsyncConnection.Table<User>().ToListAsync();
        }

        public int GetUsersQuantity()
        {
            return databaseConnection.Table<User>().Count();
        }

        public Task<int> GetUsersQuantityAsync()
        {
            return databaseAsyncConnection.Table<User>().CountAsync();
        }



        public Task<User> GetUserAsync(int id)
        {
            return databaseAsyncConnection.Table<User>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> InsertUserAsync(User user)
        {
            return databaseAsyncConnection.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return databaseAsyncConnection.DeleteAsync(user);
        }


    }
}
