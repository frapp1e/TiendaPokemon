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
    public partial class Cantidad : Form
    {
        private List<Objeto> objetosTienda;
        private DataBaseManager dbManager = new DataBaseManager();

        public static Objeto ObjetoSeleccionado { get; set; }
      
        public Cantidad(List<Objeto> objetosTienda, DataBaseManager dbManager)
        {
            InitializeComponent();

            this.objetosTienda = objetosTienda;
            this.dbManager = dbManager;
        }

        private void button1_Click(object sender, EventArgs e)// boton de sumar
        {
            int cantidadComprada = Convert.ToInt32(label2.Text);
            cantidadComprada++;
            label2.Text = cantidadComprada.ToString();
        }

        private void button3_Click(object sender, EventArgs e)// boton de comprar
        {
            if (int.TryParse(label2.Text, out int cantidadComprada))
            {
                Inventario miInventario = Inventario.ObtenerInventario();
                Objeto objetoSeleccionado = Cantidad.ObjetoSeleccionado; 
                int cantidadSeleccionada = Convert.ToInt32(label2.Text);

                if (objetoSeleccionado == null)
                {
                    MessageBox.Show("No se ha seleccionado ningún objeto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult resultado = MessageBox.Show($"¿Estás seguro de que quieres comprar {cantidadSeleccionada} {objetoSeleccionado.Nombre}?", "Confirmar compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    TiendaPokemon.ComprarObjeto(objetoSeleccionado, miInventario, cantidadSeleccionada, dbManager);
                    MessageBox.Show($"Has comprado {cantidadSeleccionada} {objetoSeleccionado.Nombre} exitosamente.", "Compra exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa una cantidad válida.");
            }
        }


        private void label1_Click(object sender, EventArgs e)//donde se muestra la cantidad
        {

        }

        private void button2_Click(object sender, EventArgs e) //boton de restar
        {
            int cantidadComprada = Convert.ToInt32(label2.Text);

            if (cantidadComprada > 0)
            {
                cantidadComprada--;
            }
            else
            {
                Console.WriteLine("La cantidad debe ser mayor a 0");
            }
            label1.Text = cantidadComprada.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void Cantidad_Load(object sender, EventArgs e)
        {

        }
    }
}
