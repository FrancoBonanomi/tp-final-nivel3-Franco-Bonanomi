using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Elemento Marca { get; set; }
        public Elemento Categoria { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public string Accion { get; set; }

        public string Clase { get; set; }

        public Articulo()
        {
            Accion = "Agregar a favoritos";
            Clase = "btn btn-primary mb-1";
        }


    }
}
