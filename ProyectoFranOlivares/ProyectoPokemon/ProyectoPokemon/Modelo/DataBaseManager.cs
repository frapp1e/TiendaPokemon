using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProyectoPokemon
{
    public class DataBaseManager
    {
        private readonly string connectionString = "server=localhost;port=3306;database=proyectopokemon;user=root;password=;";

        private MySqlConnection connection;

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public DataBaseManager()
        {
            connection = new MySqlConnection(connectionString);
        }


        public void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine("Conexión a la base de datos abierta correctamente.");
                }
                else
                {
                    Console.WriteLine("La conexión a la base de datos ya está abierta.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar abrir la conexión a la base de datos: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

        }

        public bool VerificarConexion()
        {
            try
            {
                OpenConnection();
                CloseConnection();
                return true; // La conexión se ha abierto y cerrado
            }
            catch (MySqlException)
            {
                return false; // Hubo un error al abrir la conexión
            }
        }
    }
}
