using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Fandom
{
    internal class DataProcesing
    {
        static private string dbname = "Fandom.db";
        static private string path = $"Data Source={dbname};"; // Строка подключения
        static private string show_Persons_data = "SELECT * FROM Persons;";
        static private string dbName = "Fandom";
        static private string tblPersonName = "Persons";
        static private string tblInfoName = "Info";
        static private string tblImagesName = "Images";
        //static private Binding binding; // для связи таблиц
        public static DataSet AddData()
        {
            DataSet fandom = new DataSet(dbName); // Это аналог БД, один из вариантов наполнения DataGrid 
            DataTable persons = new DataTable(tblPersonName); // виртуальная таблица в памяти
            DataColumn idColumn = new DataColumn("id", Type.GetType("System.Int32")); // Добавляем колонку
            // persons.Columns.Add("id",Type.GetType("System.Int32"));
            idColumn.Unique = true; // Уникальность - PRIMARY KEY
            idColumn.AllowDBNull = false; // true - может быть незаполненным - PRIMARY KEY
            idColumn.AutoIncrement = true;
            idColumn.AutoIncrementSeed = 100; // Начало диапазона
            idColumn.AutoIncrementStep = 1; // Шаг автоинкремента
            DataColumn nameColumn = new DataColumn("name", Type.GetType("System.String"));
            // Добавляем колонки в экземпляр DataTable
            persons.Columns.Add(idColumn);
            persons.Columns.Add(nameColumn);

            // Добавление DataTable в DataSet
            fandom.Tables.Add(persons);

            // Определяем первичный ключ
            persons.PrimaryKey = new DataColumn[] { persons.Columns["id"] };
            // 1-ый вариант добавления поля
            DataRow person = persons.NewRow(); // Экземпляр поля таблицы
            // Определяем person через массив, null - чтобы сработал автоинкремент
            person.ItemArray = new object[] { null, "Doctor David Livesey" }; 
            persons.Rows.Add(person);
            // 2-ой вариант
            persons.Rows.Add(new object[] { null, "John Silver" });
            persons.Rows.Add(new object[] { null, "William\"Billy\" Bones" });
            using (SQLiteConnection conn = new SQLiteConnection(path))
            { // Адаптер нужен для загрузки схемы БД. Может содержать несколько DataSet. Позволяет работать с собой как с БД
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(show_Persons_data, conn); // Запрос на схему БД
                SQLiteCommandBuilder sQLiteCommandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Update(fandom.Tables[0]); // Обновление sql-запросов без синтаксиса sql
                
            }
            return fandom; // возвращаем то, что обновили в БД
        }
        public static DataSet GetDBData() // Метод для обновления (обращаетсяв БД и возвращает то, что есть)
        {
            DataSet result = new DataSet();
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(show_Persons_data, conn);
                SQLiteCommandBuilder CommandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Fill(result);
            }
            return result;
        }
    }
}
