using System;
using System.Collections.Generic;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace danonchik
{
    class database
    {
        private static MySqlConnection db_connection;
        private String _host { get; set; }
        private String _user { get; set; }
        private String _pass { get; set; }
        private String _base { get; set; }

        private database()
        {
            this._host = "localhost";
            this._user = "root";
            this._pass = "root";
            this._base = "danon_ragemp";
        }
        public static void InitConnection()
        {
            database sql = new database();
            String sqlConnection = $"SERVER={sql._host}; DATABASE={sql._base}; UID={sql._user}; PASSWORD={sql._pass}";
            db_connection = new MySqlConnection(sqlConnection);
            try
            {
                db_connection.Open();
                NAPI.Util.ConsoleOutput("Успешное подключение к DB");
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput("Неудачное подключение к DB");
                NAPI.Util.ConsoleOutput("Исключение: " + ex);
                NAPI.Task.Run(() =>
                {
                    Environment.Exit(-1);
                }, delayTime: 5000);
            }
        }
        public static bool IsAccountExists(string name)
        {
            MySqlCommand command = db_connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts WHERE login=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using (MySqlDataReader reader = command.ExecuteReader()) {
                if (reader.HasRows)
                {
                    return true;
                }
                else return false;
            }
        }
        public static bool Login(string name, string password)
        {
            MySqlCommand command = db_connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts WHERE login=@name AND pass=@pass";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@pass", password);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else return false;
            }
        }
        public static bool Reg(string name, string password)
        {
            MySqlCommand command = db_connection.CreateCommand();
            command.CommandText = "INSERT INTO accounts VALUES (@name, @pass, 1000);";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@pass", password);

            using (MySqlDataReader reader = command.ExecuteReader())
            { }
            MySqlCommand check = db_connection.CreateCommand();
            check.CommandText = "SELECT * FROM accounts WHERE login=@name AND pass=@pass";
            check.Parameters.AddWithValue("@name", name);
            check.Parameters.AddWithValue("@pass", password);
            using (MySqlDataReader reader = check.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}