using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProyectoPokemon
{
    public class Inventario
    {
        public int LimiteInventario = 200;
        private Dictionary<string, Objeto> ObjetosInventario = new Dictionary<string, Objeto>(); // Así no se duplican los objetos
        private static Inventario instancia;

        private Inventario()
        {
            ObjetosInventario = new Dictionary<string, Objeto>(); // Diccionario vacío
        }
        public static Inventario ObtenerInventario()
        {
            if (instancia == null)
            {
                instancia = new Inventario();
            }
            return instancia;
        }

   
        // Agregar objeto solo si lo ha comprado
        public void AddObjeto(Objeto objeto, int cantidad, DataBaseManager dbManager)
        {
            // Verificar si hay suficiente espacio total para el nuevo objeto
            if (ContarTotal() + cantidad <= LimiteInventario)
            {
                // Verificar si el objeto ya está en el inventario
                if (!ObjetosInventario.ContainsKey(objeto.Nombre))
                {
                    // Si el objeto no está en el inventario se agregarlo con la cantidad especificada
                    objeto.StockInventario = cantidad; // Inicio StockInventario
                    ObjetosInventario.Add(objeto.Nombre, objeto); // StockInventario es la cantidad añadida
                }
                else
                {
                    // Si el objeto ya está en el inventario, actualizar su cantidad
                    ObjetosInventario[objeto.Nombre].StockInventario += cantidad;
                }
            }
            else
            {
                MessageBox.Show("No hay suficiente espacio en el inventario para agregar este objeto.", "Límite de Inventario Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public int ContarTotal()
        {
            int contador = 0;
            foreach (Objeto obj in ObjetosInventario.Values)
            {
                contador += obj.Stock; // Cuenta todo, por eso no está el if
            }
            return contador;
        }

        // Cuando toque el botón de papelera
        public void BorrarObjeto(Objeto objeto) // item ha de tener un stock negativo (lo que vayas a quitar)
        {
            if (ObjetosInventario.ContainsKey(objeto.Nombre))
            {
                if (ObjetosInventario[objeto.Nombre].Stock + objeto.Stock < 0)
                {
                    // Eliminar el objeto del diccionario
                }
                else
                {
                    ObjetosInventario[objeto.Nombre].Stock += objeto.Stock;
                }
            }
        }

        // Para cuando aumente el límite 50 espacios
        public void AumentarLimite(DataBaseManager dbManager)
        {
            LimiteInventario += 50;
            Console.WriteLine($"El límite del inventario se ha aumentado en 50 espacios. Nuevo límite: {LimiteInventario}");
            string query = "UPDATE Inventario SET LimiteInventario = @LimiteInventario";
            using (var command = new MySqlCommand(query, dbManager.Connection))
            {
                command.Parameters.AddWithValue("@LimiteInventario", LimiteInventario);
                command.ExecuteNonQuery();
            }
        }

       
    }
}
