using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derby.ConnectionClass
{
    class Connection

    {
        public MySqlConnection connectDB;
        private string Option;
        public Connection()
        {
            string host = "localhost";
            string database = "tests";
            string username = "root";
            string password = "";
            string port = "3306";
            string connection_string = "datasource = " + host + "; database = " + database + "; port = " + port + "; username=" + username + "; password=" + password + "; SslMode = none";
            connectDB = new MySqlConnection(connection_string);
        }

        public void SetData(string Option)
        {
            this.Option = Option;
        }
        public string getOption()
        {
            return Option;
        }
    }
}
