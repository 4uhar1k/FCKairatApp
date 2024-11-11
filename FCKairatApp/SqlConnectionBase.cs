using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp
{
    public class SqlConnectionBase
    {
        string DataBasePath = "kairatdatabase.db3";
        public ISQLiteAsyncConnection CreateConnection()
        {
            return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory,DataBasePath),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }
}
