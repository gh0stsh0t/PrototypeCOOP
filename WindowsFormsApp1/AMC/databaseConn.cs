using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AMC
{
    class databaseConn
    {
        private DataTable holder;
        private MySqlConnection databasecon;
        private MySqlDataAdapter listener;
        //private MySqlCommand query;
        public databaseConn()
        {
            databasecon = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;");
        }

        public DataTable getData()
        {
            return holder;
        }

        public dynamic query(int type, string table, string fields, string where)
        {
            switch (type)
            {
                case 1:
                    return query(table, fields, where);
                case 2:
                    query(table, fields,where);
                    break;
                case 3:
                    query();
                    break;
                default:
                    break;
            }
        }
        public DataTable query(string table, string fields,string where)
        {
            string queryHolder = "SELECT "+ string.Join(", ", fields.Split(' '))+" FROM "+table;

        }

        public bool query(string tables, string fields,string where)
        {
            
        }
    }
}
