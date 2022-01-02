using ManufacturingDefectsTraking.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturingDefectsTraking.LocalDB
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            //Establishing the conection
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<VisualNoteItem>().Wait();
        }
        // Show the visual items
        public Task<List<VisualNoteItem>> GetItemsAsync()
        {
            return _database.Table<VisualNoteItem>().ToListAsync();
        }

        // Save Item
        public Task<int> SaveItemAsync(VisualNoteItem visualNoteItem)
        {
            return _database.InsertAsync(visualNoteItem);
        }

        // Delete Item
        public Task<int> DeleteItemAsync(VisualNoteItem visualNoteItem)
        {
            return _database.DeleteAsync(visualNoteItem);
        }

        // Edit Item
        public Task<int> UpdateItemAsync(VisualNoteItem visualNoteItem)
        {
            return _database.UpdateAsync(visualNoteItem);
        }
    }


}

