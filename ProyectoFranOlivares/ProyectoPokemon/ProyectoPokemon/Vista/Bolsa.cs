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
    public partial class Bolsa : Form
    {
        private DataBaseManager dbManager;
        private Inventario miInventario;
        public Bolsa(DataBaseManager dataBaseManager, Inventario inventario)
        {
            InitializeComponent();
            dbManager = dataBaseManager;
            miInventario = inventario;  
            LoadData();
        }

        private void LoadData()
        {
            
                dbManager.OpenConnection();
                string query = "SELECT o.Nombre, i.Cantidad FROM inventario i INNER JOIN objeto o ON i.IdObjeto = o.Id";
                MySqlCommand command = new MySqlCommand(query, dbManager.Connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                // Agregar columna para LimiteInventario
                DataColumn limiteColumn = new DataColumn("LimiteInventario", typeof(int));
                limiteColumn.DefaultValue = Inventario.ObtenerInventario().LimiteInventario;
                dt.Columns.Add(limiteColumn);

                dbManager.CloseConnection();
            
        }





        private void button4_Click(object sender, EventArgs e)//Boton de atras
        {
            this.Hide();
        }

        private void Bolsa_Load(object sender, EventArgs e)
        {
            LoadData();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
