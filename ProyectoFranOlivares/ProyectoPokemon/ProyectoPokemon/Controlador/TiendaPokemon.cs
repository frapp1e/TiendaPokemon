using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPokemon
{
    public class TiendaPokemon
    {
        private static DataBaseManager dbManager;
        private static List<Objeto> objetosTienda;

        public TiendaPokemon(DataBaseManager dataBaseManager)
        {
            dbManager = dataBaseManager;
        }

        //metodo para obtener la lista en otras clases o formularios
        public static List<Objeto> ObtenerObjetosTienda() 
        {
            if (objetosTienda == null)
            {
                objetosTienda = new List<Objeto>();
                objetosTienda.Add(Objeto.Pokeball);
                objetosTienda.Add(Objeto.Superball);
                objetosTienda.Add(Objeto.Ultraball);

            }
            return objetosTienda;
        }
   


        //Comprar cosas de la tienda y meterlas al inventario (utilizar funcion de Objeto decrementarStock)
        public static void ComprarObjeto(Objeto objeto, Inventario inventario, int cantidad, DataBaseManager dbManager)
        {
            //Verificar si se pasa del limite del inventario o del Stock
            if (inventario.ContarTotal() + cantidad > inventario.LimiteInventario)
            {
                MessageBox.Show("No puedes comprar esta cantidad de objetos porque excede el límite del inventario.", "Límite de Inventario Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (objeto.Stock < cantidad)
            {
                MessageBox.Show("No hay suficiente stock de este objeto.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //comandoos SQL
            try
            {
                dbManager.OpenConnection();

                // Verificar si el objeto ya está en el inventario
                string queryCheck = "SELECT COUNT(*) FROM inventario WHERE IdObjeto = @IdObjeto";
                MySqlCommand commandCheck = new MySqlCommand(queryCheck, dbManager.Connection);
                commandCheck.Parameters.AddWithValue("@IdObjeto", objeto.Id);

                int count = Convert.ToInt32(commandCheck.ExecuteScalar());

                if (count > 0)
                {
                    // Si el objeto ya está en el inventario, actualizar la cantidad
                    string queryUpdate = "UPDATE inventario SET Cantidad = Cantidad + @Cantidad WHERE IdObjeto = @IdObjeto";
                    MySqlCommand commandUpdate = new MySqlCommand(queryUpdate, dbManager.Connection);
                    commandUpdate.Parameters.AddWithValue("@Cantidad", cantidad);
                    commandUpdate.Parameters.AddWithValue("@IdObjeto", objeto.Id);
                    commandUpdate.ExecuteNonQuery();
                }
                else
                {
                    // Si el objeto no está en el inventario, insertarlo
                    string queryInsert = "INSERT INTO inventario (IdObjeto, Cantidad, LimiteInventario) VALUES (@IdObjeto, @Cantidad, @LimiteInventario)";
                    MySqlCommand commandInsert = new MySqlCommand(queryInsert, dbManager.Connection);
                    commandInsert.Parameters.AddWithValue("@IdObjeto", objeto.Id);
                    commandInsert.Parameters.AddWithValue("@Cantidad", cantidad);
                    commandInsert.Parameters.AddWithValue("@LimiteInventario", inventario.LimiteInventario);
                    commandInsert.ExecuteNonQuery();
                }

                // Actualizar la cantidad en el inventario local
                inventario.AddObjeto(objeto, cantidad, dbManager);

                // Actualizar el stock en la tabla objeto
                string queryUpdateStock = "UPDATE objeto SET Stock = Stock - @Cantidad WHERE Id = @IdObjeto";
                MySqlCommand commandUpdateStock = new MySqlCommand(queryUpdateStock, dbManager.Connection);
                commandUpdateStock.Parameters.AddWithValue("@Cantidad", cantidad);
                commandUpdateStock.Parameters.AddWithValue("@IdObjeto", objeto.Id);
                commandUpdateStock.ExecuteNonQuery();

                // Actualizar el stock en el objeto local
                objeto.Stock -= cantidad;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al insertar en la tabla Inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }





















    }

}
