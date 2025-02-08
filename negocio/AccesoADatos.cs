using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    internal class AccesoADatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader obtenerLector()
        {
            return lector;
        }
        public AccesoADatos()
        {
            //LOCAL
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true");
            //EN EL HOSTING
            //conexion = new SqlConnection("workstation id=CATALOGO_ARTICULOS.mssql.somee.com;packet size=4096;user id=FranBonanomi_SQLLogin_5;pwd=m88ralbw5c;data source=CATALOGO_ARTICULOS.mssql.somee.com;persist security info=False;initial catalog=CATALOGO_ARTICULOS;TrustServerCertificate=True");
            comando = new SqlCommand();
            comando.Connection = conexion;
        }
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void ejecutarLectura()
        {
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setearParametro(string parametro, object valor)
        {
            comando.Parameters.AddWithValue(parametro, valor);
        }

        public void ejecutarAccion()
        {
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int ejecutarAccionEscalar()
        {
            try
            {
                conexion.Open();
                return (int)comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void cerrarConexion()
        {
            conexion.Close();
            if (lector != null)
                lector.Close();
        }
    }
}
