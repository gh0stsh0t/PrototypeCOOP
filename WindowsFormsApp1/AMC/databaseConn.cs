using System;
using System.Data;
using System.Windows.Forms;
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
            RefreshCmd();
        }

        public DataTable storedProc(string procName)
        {
            RefreshCmd();
            using (var databasecon = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;"))
            {
                databasecon.Open();
                _cmd.CommandText = procName;
                _cmd.Connection = databasecon;
                _cmd.CommandType = CommandType.StoredProcedure;
                _holder.Load(_cmd.ExecuteReader());
                return _holder;
            }
        }

        public DataTable storedProc(string procName, string paraName, int x, string paraName2, int y)
        {
            RefreshCmd();
            using (var databasecon = new MySqlConnection("Server=localhost;Database=amc;Uid=root;Pwd=root;"))
            {
                databasecon.Open();
                _cmd.CommandText = procName;
                _cmd.Connection = databasecon;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                MySqlParameter param = new MySqlParameter(paraName, x);
                _cmd.Parameters.Add(param);
                param = new MySqlParameter(paraName2, y);
                _cmd.Parameters.Add(param);
                _holder.Load(_cmd.ExecuteReader());
                return _holder;
            }
        }

        public string lastID()
        {
            return _cmd.LastInsertedId.ToString();
        }
        private void RefreshCmd()
        {
            _cmd = new MySqlCommand();
            _holder = new DataTable();
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
                _holder.Load(_cmd.ExecuteReader());
                return _holder;
            }
        }
        //wait
        /*
        public dynamic GetQueryData(int x)
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
                object tae.Load(_cmd.ExecuteScalar());
                return _holder;
            }
        }
        */
        public DataTable GetData()
        {
            return _holder;
        }

        public MySqlCommand getCmd()
        {
            return _cmd;
        }

        public DatabaseConn Select(string table, params string[] fields)
        {
            _flag = false;
            RefreshCmd();
            _sql = "SELECT " + string.Join(", ", fields) + " FROM " + table + " ";
            return this;
        }

        public DatabaseConn Select(string table, string[]fields, string[] wheres)
        {
            return Select(table, fields).Where(wheres);
        }

        public DatabaseConn Update(string table, params string[] fields)
        {
            if (fields.Length % 2 == 0)
            {
                RefreshCmd();
                _flag = true;
                _sql = "UPDATE " + table + " SET ";
                for (var i = 0; i < fields.Length; i++)
                {
                    _sql += fields[i] + " = @" + fields[i] + (i + 2 < fields.Length ? " ,    " : " ");
                    _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]); 
                    //Every other parameter added as parameterized value
                }
                return this;
            }
            throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
        }

        public DatabaseConn Update(string table, string[] fields, string[] wheres)
        {
            return Update(table, fields).Where(wheres);
        }

        public DatabaseConn Insert(string table, params string[] fields)
        {
            if (fields.Length % 2 == 0)
            {
                RefreshCmd();
                _flag = true;
                _sql = "INSERT INTO " + table + " (";
                var values = "VALUES (";
                for (var i = 0; i < fields.Length; i++)
                {
                    _sql += fields[i] + (i + 2 < fields.Length ? " ,    " : ") ");
                    values += "@" + fields[i] + (i + 2 < fields.Length ? " ,    " : ") ");
                    _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]); 
                    //Every other parameter added as parameterized value
                }
                _sql += values;
                return this;
            }
            throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
        }

        public DatabaseConn Where(params string[] fields)
        {
            if (fields.Length % 2 == 0)
            {
                _sql += "WHERE ";
                for (var i = 0; i < fields.Length; i++)
                {
                    _sql += fields[i] + " = @" + fields[i] + (i + 2 < fields.Length ? " AND " : "");
                    _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]); 
                    //Every other parameter added as parameterized value
                }
                return this;
            }
            throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
        }
    }
}