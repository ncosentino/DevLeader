using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace DataLayer.Sqlite
{
    public class SqliteModel : IModel
    {
        #region Fields

        private readonly SQLiteConnection _connection;

        #endregion

        #region Constructors

        private SqliteModel(string dataSource)
        {
            _connection = new SQLiteConnection(string.Format("Data Source=\"{0}\"", dataSource));
            _connection.Open();
        }

        ~SqliteModel()
        {
            Dispose();
        }

        #endregion

        #region Exposed Members

        public static IModel Create(string dataSource)
        {
            return new SqliteModel(dataSource);
        }

        public bool Exists(string name)
        {
            using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='" + name + "'", _connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public void Insert(string name, IDictionary<string, object> values)
        {
            if (values.Count < 1)
            {
                throw new ArgumentException("Values cannot be empty.", "values");
            }

            var builder = new StringBuilder();
            builder.Append("INSERT INTO [").Append(name).Append("] (");

            using (var cmd = new SQLiteCommand(_connection))
            {
                foreach (var column in values.Keys)
                {
                    builder.Append("[").Append(column).Append("],");
                }

                builder.Length--;
                builder.Append(") VALUES (");

                foreach (var kvp in values)
                {
                    builder.Append("?,");
                    cmd.Parameters.Add(new SQLiteParameter() { Value = kvp.Value });
                }

                builder.Length--;
                builder.Append(")");

                cmd.CommandText = builder.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        public void Create(string name, IDictionary<string, string> definitions)
        {
            if (definitions.Count < 1)
            {
                throw new ArgumentException("Definitions cannot be empty.", "definitions");
            }

            var builder = new StringBuilder();
            builder.Append("CREATE TABLE [").Append(name).Append("] (");

            foreach (var kvp in definitions)
            {
                builder.Append("[").Append(kvp.Key).Append("] ").Append(kvp.Value).Append(",");
            }

            builder.Length--;
            builder.Append(")");

            using (var cmd = new SQLiteCommand(builder.ToString(), _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }

        #endregion
    }
}
