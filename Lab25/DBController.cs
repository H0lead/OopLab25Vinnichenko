using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab25
{
    internal class DBController
    {
        // Шлях до бази даних. При тестуванні змінити.
        private string path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database\SportsDB.accdb;";

        // Метод для отримання з'єднання з базою даних.
        public OleDbConnection GetConnection()
        {
            return new OleDbConnection(path);
        }

        // Метод для отримання інформації даних з бази даних. Блок using використовується для автоматичного закриття з'єднання
        // Формується SQL запит у рядку query. Цей SQL запит разом з з'єднанням з базою даних передається у адаптер, з адаптера ми зповюємо
        // об'єкт класу DataTable, з якого потім заповюємо самий DataGridView.
        public void getAllInto(OleDbConnection oleDbConnection, DataGridView dataGridView)
        {
            using (oleDbConnection)
            {
                string query = "SELECT * FROM sports";

                OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView.DataSource = dataTable;
            }
        }

        // Метод додавання запису у базу даних. Знову ж таки формується SQL запит, через параметри встановлюється ім'я, опис та isOlymplic
        // з об'єкту sport який ми передали, відкривається з'єднання та відбувається вставка. З'єднання закривається.
        public void add(OleDbConnection oleDbConnection, Sport sport)
        {
            using (oleDbConnection) 
            {
                string query = "INSERT INTO sports ([name],[description],[is_olympic]) VALUES (?, ?, ?)";

                OleDbCommand dbCommand = new OleDbCommand(query, oleDbConnection);

                dbCommand.Parameters.AddWithValue("?", sport.Name);
                dbCommand.Parameters.AddWithValue("?", sport.Description);
                dbCommand.Parameters.AddWithValue("?", sport.IsOlympic);

                oleDbConnection.Open();
                dbCommand.ExecuteNonQuery();
                oleDbConnection.Close();
            }
        }

        // Та ж логіка що й у додаванні, але тут другий SQL запит.
        public void delete(OleDbConnection oleDbConnection, int id)
        {
            using (oleDbConnection)
            {
                string query = "DELETE FROM sports WHERE id = ?";

                OleDbCommand oleDbCommand = new OleDbCommand(query, oleDbConnection);

                oleDbCommand.Parameters.AddWithValue("?", id);

                oleDbConnection.Open();
                oleDbCommand.ExecuteNonQuery();
                oleDbConnection.Close();
            }
        }

        // Знову ж таки така ж логіка, але тут пошук йде по айді, яке ми передаємо в останню чергу.
        public void update(OleDbConnection oleDbConnection, Sport sport, int id)
        {
            using (oleDbConnection)
            {
                string query = "UPDATE sports SET [name] = ?, [description] = ?, [is_olympic] = ? WHERE [id] = ?";

                OleDbCommand oleDbCommand = new OleDbCommand(query, oleDbConnection);

                oleDbCommand.Parameters.AddWithValue("?", sport.Name);
                oleDbCommand.Parameters.AddWithValue("?", sport.Description);
                oleDbCommand.Parameters.AddWithValue("?", sport.IsOlympic);
                oleDbCommand.Parameters.AddWithValue("?", id);

                oleDbConnection.Open();
                oleDbCommand.ExecuteNonQuery();
                oleDbConnection.Close();

            }
        }
    }
}
