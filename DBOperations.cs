using SQLite;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XF_GoldenStore.Model
{
    public class DBOperations
    {
        readonly SQLiteAsyncConnection db;

        public DBOperations(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Users>().Wait();
            db.CreateTableAsync<Products>().Wait();
        }


        // Get a specific user by Username.
        public Task<Users> LoginAsync(string username, string password)
        {
            return db.Table<Users>().Where(i => i.Username == username && i.Password == password).FirstOrDefaultAsync();
        }
        // Get a specific user by Username.
        public Task<Users> GetUserAsync(int id)
        {
            return db.Table<Users>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(Users user)
        {
            if (user.Id != 0)
            {
                // Update an existing User.
                return db.UpdateAsync(user);
            }
            else
            {
                // Save a new User.
                return db.InsertAsync(user);
            }
        }
       
        // Delete User.
        public Task<int> DeleteUserAsync(Users user)
        {
            return db.DeleteAsync(user);
        }
        //Get all Users.
        public Task<List<Users>> GetAllUsersAsync()
        {
            return db.Table<Users>().ToListAsync();
        }

        //==========    Products     ===================

        public Task<int> SaveProductAsync(Products product)
        {
            if (product.Id != 0)
            {
                // Update an existing User.
                return db.UpdateAsync(product);
            }
            else
            {
                // Save a new User.
                return db.InsertAsync(product);
            }
        }

        // Get a specific Product by Mobile.
        public Task<Products> GetProductAsync(string mobile)
        {
            return db.Table<Products>().Where(i => i.Mobile == mobile ).FirstOrDefaultAsync();
        }

        public Task<Products> GetGoldAsync(int id)
        {
            return db.Table<Products>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<List<Products>> GetAllProductForUserAsync(string mobile)
        {
            return db.Table<Products>().Where(i => i.Mobile == mobile).ToListAsync();
        }
        public Task<Products> GetOneProductAsync(string productName)
        {
            return db.Table<Products>().Where(i => i.ProductName == productName).FirstOrDefaultAsync();
        }


    }
}
