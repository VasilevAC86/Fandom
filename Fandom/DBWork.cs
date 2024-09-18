using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fandom
{
    internal class DBWork
    {
        static private string dbname = "Fandom.db";
        static private string path = $"Data Source={dbname};";
        static private List<string> queryes = new List<string>();
        static private List<SQLiteCommand> commands = new List<SQLiteCommand>();
        static public void FillQueryes(string filename = @"sql\CreateDB.sql")
        {
            queryes = FSWork.ReadSQLFile(filename);
        }
        static public bool MakeQuery()
        {
            bool result = false;
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                conn.Open();
                for (int i = 0; i < queryes.Count; i++)
                {
                    commands.Add(conn.CreateCommand());
                    commands[i].CommandText = queryes[i];
                    commands[i].ExecuteNonQuery();
                }
            }
            result = true;
            return result;
        }
        static public List<string> GetMechanics()
        {
            List<string> result = new List<string>();
            string get_mechanisc = "SELECT name From Mechanics;";

            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = get_mechanisc;
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
            }
            return result;
        }
        static public void AddAvatar(string _name, byte[] _image)
        {
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = @"UPDATE Mechanics SET Avatar=@Avatar " +
                    $"WHERE Name LIKE '{_name}%';";
                command.Parameters.Add(new SQLiteParameter("@Avatar", _image));
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
        static public MemoryStream GetAvatar(string _name)
        {
            MemoryStream result = null;
            byte[] _image = null;
            using (SQLiteConnection conn =
                new SQLiteConnection(path))
            {
                SQLiteCommand cmd = new SQLiteCommand(conn);
                string get_image = $"SELECT Avatar FROM Mechanics WHERE Name LIKE '{_name}%';";
                cmd.CommandText = get_image;
                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            _image =
                            (byte[])reader.GetValue(0);
                        }

                    }
                }
            }
            if (_image != null)
            {
                result = new MemoryStream(_image);
            }
            return result;
        }
        static public void AddData(string _newCategoryInsert,
            string _dbname = "test02")
        {
            string path = $"Data Source={_dbname};";
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                SQLiteCommand cmd = new SQLiteCommand(conn);
                cmd.CommandText = _newCategoryInsert;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static public DataSet Refresh(string _dbname = "test02")
        {
            DataSet result = new DataSet();
            string path = $"Data Source={_dbname};";
            string show_all_data = "SELECT * FROM Category;";
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                conn.Open();
                SQLiteDataAdapter adapter =
                    new SQLiteDataAdapter(show_all_data, conn);
                adapter.Fill(result);
            }
            return result;
        }
        static public void Save(DataTable dt,
            out string _query,
            string _dbname = "test02")
        {
            string path = $"Data Source={_dbname};";
            string show_all_data = "SELECT * FROM Category;";
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                conn.Open();
                SQLiteDataAdapter adapter =
                    new SQLiteDataAdapter(show_all_data, conn);
                SQLiteCommandBuilder commandBuilder =
                    new SQLiteCommandBuilder(adapter);
                adapter.Update(dt);
                _query = commandBuilder.GetUpdateCommand().CommandText;
            }
        }
    }
}
