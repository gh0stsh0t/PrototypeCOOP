using System.Data;
using MySql.Data.MySqlClient;

namespace AMC
{
    internal class DatabaseConn
    {
        private DataTable _holder;
        private MySqlCommand _cmd;

        private string _sql = ""; 
        private bool _flag;   
        public DatabaseConn()
        {
            _cmd=new MySqlCommand();
        }

        public dynamic GetQueryData()
        {
            using (var databasecon = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;"))
            {
                databasecon.Open();
                _cmd.CommandText = _sql;
                _cmd.Connection = databasecon;
                if (_flag)
                {
                    _cmd.ExecuteNonQuery();
                    return _flag;
                }
                else
                {
                    _holder.Load(_cmd.ExecuteReader());
                    return _holder;
                }
            }
        }

        public DataTable GetData()
        {
            return _holder;
        }
        public DatabaseConn Select(string table, params string[] fields)
        {
            _flag = false;
            _sql = "SELECT " + string.Join(", ", fields) + " FROM " + table + " ";
            return this;
        }

        public DatabaseConn Update(string table, params string[] fields)
        {
            _flag = true;
            _sql = "UPDATE " + table + " SET ";
            for (int i = 0; i < fields.Length; i++)
            {
                _sql += fields[i] + " = @" + fields[i] + ((i + 2 < fields.Length) ? " ,    " : " ");
                _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]); //Every other parameter added as parameterized value
            }
            return this;
        }

        public DatabaseConn Insert(string table, params string[] fields )
        {
            _flag = true;
            _sql = "INSERT INTO " + table + " (";
            string values = "VALUES (";
            for (int i = 0; i < fields.Length; i++)
            {
                _sql += fields[i] + ((i + 2 < fields.Length) ? " ,    " : ") ");
                values += "@" + fields[i] + ((i + 2 < fields.Length) ? " ,    " : ") ");
                _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]); //Every other parameter added as parameterized value
            }
            _sql += values;
            return this;
        }

        public DatabaseConn Where(params string[] fields)
        {
            _sql += "WHERE ";
            for ( int i = 0; i < fields.Length; i++)
            {
                _sql += fields[i] + " = @" + fields[i]+ ((i + 2 < fields.Length) ? " AND " : "");
                _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]); //Every other parameter added as parameterized value
            }
            return this;
        }
    }
}
