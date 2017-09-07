using System.Data;
using MySql.Data.MySqlClient;

namespace AMC
{
    public class databaseConnection
    {
        public DataTable holder;
        public MySqlConnection databasecon;
        public MySqlDataAdapter listener;
        public MySqlCommand query;

        public databaseConnection(MySqlConnection databasecon)
        {
            this.databasecon = databasecon;
        }

        public databaseConnection()
        {
        }
    }
}