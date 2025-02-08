using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UsuarioNegocio
    {

        private AccesoADatos datos;

        public int registrarUsuario(Usuario aRegistrar)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("INSERT INTO USERS (email,pass,nombre,apellido) output inserted.Id values(@email,@pass,@nom,@ape)");
                datos.setearParametro("@email", aRegistrar.Email);
                datos.setearParametro("@pass", aRegistrar.Pass);
                datos.setearParametro("@nom", aRegistrar.Nombre);
                datos.setearParametro("@ape", aRegistrar.Apellido);
                return datos.ejecutarAccionEscalar();
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


        public bool loguearUsuario(Usuario aLoguear, string parametroPass = "")
        {
            try
            {
                datos = new AccesoADatos();
                string consulta = "SELECT Id,admin,nombre,apellido,email,urlImagenPerfil from USERS WHERE email=@email ";
                if (parametroPass != "")
                    consulta += parametroPass;
                datos.setearConsulta(consulta);

                datos.setearParametro("@email", aLoguear.Email);
                if (parametroPass != "")
                    datos.setearParametro("@pass", aLoguear.Pass);
                datos.ejecutarLectura();
                SqlDataReader lector = datos.obtenerLector();
                if (lector.Read())
                {
                    aLoguear.Id = (int)lector["Id"];
                    aLoguear.Admin = (bool)lector["admin"];
                    aLoguear.Email = (string)lector["email"];
                    if (!validarNulo(lector["nombre"]))
                        aLoguear.Nombre = (string)lector["nombre"];
                    if (!validarNulo(lector["apellido"]))
                        aLoguear.Apellido = (string)lector["apellido"];
                    if (!validarNulo(lector["urlImagenPerfil"]))
                        aLoguear.Imagen = (string)lector["urlImagenPerfil"];

                    return true;
                }
                return false;
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


        private bool validarNulo(object lectura)
        {
            return lectura is DBNull;
        }


        public void actualizarUsuario(Usuario aActualizar)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("UPDATE USERS SET nombre=@nom,apellido=@ape,urlImagenPerfil=@url where Id=" + aActualizar.Id);
                datos.setearParametro("@nom", aActualizar.Nombre);
                datos.setearParametro("@ape", aActualizar.Apellido);
                datos.setearParametro("@url", aActualizar.Imagen ?? (object)DBNull.Value);
                datos.ejecutarAccion();
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

        public void quitarImagen(int id)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("update USERS SET urlImagenPerfil=@url where Id=" + id);
                datos.setearParametro("@url", DBNull.Value);
                datos.ejecutarAccion();
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
