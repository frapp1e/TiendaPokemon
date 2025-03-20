using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPokemon
{
    public class Objeto
    {
        public int Id {  get; }
        public String Nombre { get; set; }
        public int Stock { get; set; }
        public int Precio { get; set; }
        public int StockInventario { get; set; }



        public Objeto(int id, String nombre, int stock, int precio, int stockinventario )
        {
            Id = id;
            Nombre = nombre;
            Stock = stock;
            Precio = precio;
            StockInventario = stockinventario;  
        }

        public override String ToString()
        {
            return $"Nombre:{Nombre}, Stock:{Stock}";
        }

        //instancia
       public static Objeto Pokeball { get; } = new Objeto(1,"Pokeball", 50, 1,0);
       public static Objeto Superball { get; } = new Objeto(2,"Superball", 50, 5, 0);
       public static Objeto Ultraball { get; } = new Objeto(3,"Ultraball", 50, 10, 0);
       


        //Por si coge mas de un item a la vez que le devuelva el precio
        public int CalcularPrecioTotal(int cantidad)
        {
            return Precio * cantidad;
        }


    }

 
}
