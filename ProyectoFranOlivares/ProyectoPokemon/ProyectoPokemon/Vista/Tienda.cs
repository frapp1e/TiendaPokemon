using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ProyectoPokemon
{
    public partial class Tienda : Form
    {
        private List<Objeto> objetosTienda;
        private Inventario inventario;
        private DataBaseManager dbManager;

        public Tienda(DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            objetosTienda = TiendaPokemon.ObtenerObjetosTienda();
            dbManager = dataBaseManager;
            inventario= Inventario.ObtenerInventario();
        }
        private void StockLabels()
        {
            try
            {
                dbManager.OpenConnection();

                // Obtener el stock de Pokeballs
                string queryPokeball = "SELECT Stock FROM objeto WHERE nombre = 'Pokeball'";
                using (var command = new MySqlCommand(queryPokeball, dbManager.Connection))
                {
                    var result = command.ExecuteScalar();
                    label2.Text = result != null ? result.ToString() : "0";
                }

                // Obtener el stock de Superballs
                string querySuperball = "SELECT Stock FROM objeto WHERE nombre = 'Superball'";
                using (var command = new MySqlCommand(querySuperball, dbManager.Connection))
                {
                    var result = command.ExecuteScalar();
                    label3.Text = result != null ? result.ToString() : "0";
                }

                // Obtener el stock de Ultraballs
                string queryUltraball = "SELECT Stock FROM objeto WHERE nombre = 'Ultraball'";
                using (var command = new MySqlCommand(queryUltraball, dbManager.Connection))
                {
                    var result = command.ExecuteScalar();
                    label4.Text = result != null ? result.ToString() : "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }
        private void Cantidad_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Actualizar los labels después de que el formulario Cantidad se cierra
            StockLabels(); 
        }


        private void button1_Click(object sender, EventArgs e)// boton comprar superball
        {
            Objeto pokeball = objetosTienda.Find(objeto => objeto.Nombre == "Superball");
            if (pokeball != null)
            {
                Cantidad cantidadForm = new Cantidad(objetosTienda, dbManager);
                Cantidad.ObjetoSeleccionado = pokeball;
                cantidadForm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e) //boton de comprar pokeball
        {
            Objeto superball = objetosTienda.Find(objeto => objeto.Nombre == "Pokeball");
            if (superball != null)
            {
                Cantidad cantidadForm = new Cantidad(objetosTienda, dbManager);
                Cantidad.ObjetoSeleccionado = superball;
                cantidadForm.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)//boton comprar ultraball
        {
            Objeto ultraball = objetosTienda.Find(objeto => objeto.Nombre == "Ultraball");
            if (ultraball != null)
            {
                Cantidad cantidadForm = new Cantidad(objetosTienda, dbManager);
                Cantidad.ObjetoSeleccionado = ultraball;
                cantidadForm.ShowDialog();
            }
        }




        private void button4_Click_1(object sender, EventArgs e)//boton de atras
        {
            this.Hide();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Tienda_Load(object sender, EventArgs e)
        {
            StockLabels();

        }

        private void button5_Click(object sender, EventArgs e) // botón de comprar espacio
        {
            try
            {
                dbManager.OpenConnection();
                inventario.AumentarLimite(dbManager);
                MessageBox.Show("El límite del inventario se ha aumentado en 50 espacios.", "Límite Aumentado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aumentar el límite del inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            StockLabels();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            StockLabels();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            StockLabels();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
