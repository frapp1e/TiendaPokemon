using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPokemon
{
    public partial class Form1 : Form
    {
        private DataBaseManager dbManager;


        public Form1()
        {
            InitializeComponent();

            dbManager= new DataBaseManager();
        }
        //Todos las funciones que le pones (nombreformulario + load) cada vez que las inicas se ejecuta la funcion
        private void form1_load(object sender, EventArgs e)
        {
            dbManager.OpenConnection(); //abrir conexion BD

            //si la conexion se abre correctamete
            if(dbManager.Connection.State != ConnectionState.Open)
            {
                MessageBox.Show("No se pudo abrir la conexión a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

    
    
        }
        private void button2_Click(object sender, EventArgs e)//boton de la bolsa
        {
            Inventario miInventario = Inventario.ObtenerInventario();
            Bolsa bolsa = new Bolsa(dbManager, miInventario);
            bolsa.Show();

            //BASE DE DATOS
            dbManager.OpenConnection();
            string query = "SELECT o.Nombre, o.StockInventario, i.Cantidad FROM inventario i INNER JOIN Objeto o ON i.IdObjeto = o.Id\r\n";
            MySqlCommand command = new MySqlCommand(query, dbManager.Connection);

            try
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Procesar los datos recuperados de la base de datos
                            string nombre = reader.GetString("Nombre");
                            int stockInventario = reader.GetInt32("StockInventario");
                            int cantidad = reader.GetInt32("Cantidad");

                            // Realizar las operaciones necesarias con los datos
                            // Por ejemplo, mostrarlos en un MessageBox
                            MessageBox.Show($"Nombre: {nombre}, Stock Inventario: {cantidad}");
                        }
                    }
                    else
                    {
                        // Mostrar un mensaje si no hay filas en el resultado
                        MessageBox.Show("No hay datos en la tabla Inventario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución de la consulta
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión a la base de datos al finalizar la operación
                dbManager.CloseConnection();
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //boton de la tienda
        {
            Tienda tienda = new Tienda(dbManager);
            tienda.Show();

            //BASE DE DATOS
            dbManager.OpenConnection();

            string query = "SELECT * FROM objeto WHERE stock > 0";
            MySqlCommand command = new MySqlCommand(query, dbManager.Connection);
            MySqlDataReader reader = command.ExecuteReader();

            TiendaPokemon tiendaPokemon = new TiendaPokemon(dbManager);
            List<Objeto> ObjetosTienda = TiendaPokemon.ObtenerObjetosTienda();

            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string nombre = reader.GetString("nombre");
                int stock = reader.GetInt32("stock");
                int precio = reader.GetInt32("precio");
                int stockinventario = reader.GetInt32("stockinventario"); //No hace falta llamarlo porque no lo voy a utilizar pero si no lo llamo me salta erropr

                Objeto objeto = new Objeto(id, nombre, stock,precio, stockinventario);
                ObjetosTienda.Add(objeto);
            }

            // Cerrar la conexión
            reader.Close();
            dbManager.CloseConnection();
        }




        private void button3_Click(object sender, EventArgs e) //boton de salir
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
