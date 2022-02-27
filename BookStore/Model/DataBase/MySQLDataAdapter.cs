using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Model.DataBase
{
    /// <summary>
    /// Class wrapper for most frequent operations with DBMS MySQL
    /// </summary>
    public class MySQLDataAdapter : IDataAdapter, IDisposable
    {
        protected MySqlConnection Connection;

        private string ValueToString(object inputObject)
        {
            if (inputObject == null)
            {
                return "NULL";
            }

            switch (Type.GetTypeCode(inputObject.GetType()))
            {
                case TypeCode.Boolean: return ((bool)inputObject) ? "1" : "0";
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal: return MySqlHelper.EscapeString(inputObject.ToString());
                case TypeCode.String:
                case TypeCode.DateTime:
                case TypeCode.Char: return $"'{MySqlHelper.EscapeString(inputObject.ToString())}'";

                default: throw new ArgumentException("Value is not simple type.");
            }
        }

        public bool IsConnected { get; private set; }

        public T GetOne<T>(string query) where T : IConvertible
        {
            T result = (T)Activator.CreateInstance(typeof(T));

            MySqlCommand command = new MySqlCommand(query, Connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                result = reader.Read()
                    ? reader.IsDBNull(0) ? default : (T)Convert.ChangeType(reader[0], typeof(T))
                    : throw new NullReferenceException("No data in the MySQL data base");
            }

            return result;
        }

        public List<Dictionary<string, string>> GetQueryResult(string query)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            MySqlCommand command = new MySqlCommand(query, Connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                //Read rows
                while (reader.Read())
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    //All fields one by one in current row
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string value = (!reader.IsDBNull(i)) ? reader.GetString(i) : "";
                        row.Add(reader.GetName(i), value);
                    }
                    result.Add(row);
                }
            }

            return result;
        }

        public int Execute(string query)
        {
            MySqlCommand command = new MySqlCommand(query, Connection);

            return command.ExecuteNonQuery();
        }

        public long InsertRow(string query)
        {
            MySqlCommand command = new MySqlCommand(query, Connection);
            command.ExecuteNonQuery();
            return command.LastInsertedId;
        }

        public Dictionary<string, string> GetIndexedList(string query)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            MySqlCommand command = new MySqlCommand(query, Connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0), reader.GetString(1));
                }
            }

            return result;
        }

        public MySQLDataAdapter()
        {
            Connection = new MySqlConnection();
        }

        public bool Connect(ConnectionSettings settings)
        {
            Connection.ConnectionString = "Server=" + settings.Host + ";port=" + settings.Port
                                        + ";User Id=" + settings.User
                                        + (!string.IsNullOrWhiteSpace(settings.Password) ? ";password=" + settings.Password : "")
                                        + ";Database=" + settings.DefaultSchema
                                        + ";CharSet=" + settings.CharSet;
            try
            {
                Connection.Open();
                IsConnected = true;
                return true;
            }
            catch (Exception)
            {
                IsConnected = false;
                return false;
            }
        }

        public void Disconnect()
        {
            Connection.Close();
            IsConnected = false;
        }

        public void Dispose()
        {
            Connection.Close();
        }

        public long InsertRow(string tableName, Dictionary<string, object> values)
        {
            StringBuilder queryBuilder = new StringBuilder($"INSERT INTO `{tableName}` (");
            bool isFirst = true;

            foreach (KeyValuePair<string, object> v in values)
            {
                if (!isFirst)
                {
                    queryBuilder.Append(" ,");
                }
                else
                {
                    isFirst = false;
                }

                queryBuilder.Append($"`{v.Key}`");
            }

            queryBuilder.Append(") VALUES (");
            isFirst = true;

            foreach (KeyValuePair<string, object> value in values)
            {
                if (!isFirst)
                {
                    queryBuilder.Append(" ,");
                }
                else
                {
                    isFirst = false;
                }

                queryBuilder.Append(ValueToString(value.Value));
            }

            queryBuilder.Append(");");

            return InsertRow(queryBuilder.ToString());
        }

        public bool DeleteRow(string tableName, long id)
        {
            return Execute($"DELETE FROM {tableName} WHERE ID={id}") == 1;
        }

        public bool UpdateRow(string tableName, long id, Dictionary<string, object> values)
        {
            StringBuilder queryBuilder = new StringBuilder($"UPDATE `{tableName}` SET ");
            bool isFirst = true;

            foreach (KeyValuePair<string, object> value in values)
            {
                if (!isFirst)
                {
                    queryBuilder.Append(" ,");
                }
                else
                {
                    isFirst = false;
                }

                queryBuilder.Append($"`{value.Key}`={ValueToString(value.Value)}");
            }

            queryBuilder.Append($" WHERE `ID`={id};");

            return Execute(queryBuilder.ToString()) == 1;
        }
    }
}
