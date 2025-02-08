using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ElementoNegocio
    {

        private AccesoADatos datos;
        public List<Elemento> listarElementos(string consulta)
        {
            try
            {
                List<Elemento> listaDeElementos = new List<Elemento>();
                datos = new AccesoADatos();
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                SqlDataReader lector = datos.obtenerLector();
                while (lector.Read())
                {
                    Elemento elemento = new Elemento();
                    elemento.Id = (int)lector["Id"];
                    elemento.Descripcion = (string)lector["Descripcion"];
                    listaDeElementos.Add(elemento);
                }

                return listaDeElementos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
