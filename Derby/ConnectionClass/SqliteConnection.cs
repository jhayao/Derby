using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace Derby.ConnectionClass
{
    
    class SqliteConnection
    {
        public SQLiteConnection con;
        public SQLiteCommand sqlite_cmd;
        public SQLiteDataReader sqlite_datareader;
        string startupPath = Environment.CurrentDirectory;
        public SqliteConnection()
        {
            con = new SQLiteConnection("Data Source="+ startupPath + "\\derby_db.db;Version=3;New=True;Compress=True;");

        }
    }
}
