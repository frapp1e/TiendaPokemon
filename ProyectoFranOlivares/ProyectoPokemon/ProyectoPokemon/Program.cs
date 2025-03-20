using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPokemon
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataBaseManager dbManager = new DataBaseManager();



            if (dbManager.VerificarConexion())
            {
                MessageBox.Show("Conexión exitosa a la base de datos.", "Conexión exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);//VERIFICADOR

                // Si la conexión es exitosa, iniciar el formulario principal
                Application.Run(new Form1());
            }
            else
            {
                // Si hay un problema con la conexión, mostrar un mensaje de error
                MessageBox.Show("No se pudo conectar a la base de datos. Por favor, revise la configuración de la conexión.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
