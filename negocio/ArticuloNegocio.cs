using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {

        private AccesoADatos datos;
        public List<Articulo> listarArticulos()
        {
            try
            {
                return traerArticulos("SELECT A.Id,Codigo,Nombre,A.Descripcion,Precio,M.Descripcion as Marca,C.Descripcion as Categoria,ImagenUrl,A.IdMarca,A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C WHERE A.IdMarca=M.Id and  A.IdCategoria=C.Id");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void quitarImagen(int id)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("update ARTICULOS SET ImagenUrl=@url where Id=" + id);
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

        public void agregarAFavoritos(int idUsuario, int idArticulo)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("insert into FAVORITOS (IdUser,IdArticulo) VALUES (@idUser,@idArticulo)");
                datos.setearParametro("@idUser", idUsuario);
                datos.setearParametro("@idArticulo", idArticulo);
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


        public List<Articulo> listarFavoritos(int idUsuario)
        {
            try
            {
                List<Articulo> listaDeArticulos = new List<Articulo>();
                datos = new AccesoADatos();
                datos.setearConsulta("SELECT A.Id, Codigo,Nombre,A.Descripcion,Precio,M.Descripcion as Marca,C.Descripcion as Categoria,ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C, FAVORITOS F WHERE A.IdMarca=M.Id and A.IdCategoria=C.Id and F.IdArticulo=A.Id and F.IdUser=" + idUsuario);
                datos.ejecutarLectura();
                SqlDataReader lector = datos.obtenerLector();
                while (lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)lector["Id"];
                    articulo.Codigo = (string)lector["Codigo"];
                    articulo.Nombre = (string)lector["Nombre"];
                    articulo.Descripcion = (string)lector["Descripcion"];
                    articulo.Marca = new Elemento();
                    articulo.Marca.Descripcion = (string)lector["Marca"];
                    articulo.Categoria = new Elemento();
                    articulo.Categoria.Descripcion = (string)lector["Categoria"];
                    if (!(lector["ImagenUrl"] is DBNull))
                        articulo.Imagen = (string)lector["ImagenUrl"];
                    articulo.Precio = (decimal)lector["Precio"];
                    listaDeArticulos.Add(articulo);
                }

                return listaDeArticulos;
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


        public void quitarDeFavoritos(int idArticulo, int idUsuario = 0)
        {
            try
            {
                datos = new AccesoADatos();
                string eliminacionPorUser = idUsuario != 0 ? "IdUser=@idUser and" : "";
                datos.setearConsulta("delete from FAVORITOS where " + eliminacionPorUser + " IdArticulo=@idArticulo");
                if (idUsuario != 0)
                    datos.setearParametro("@idUser", idUsuario);
                datos.setearParametro("@idArticulo", idArticulo);
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
        public void agregarArticulo(Articulo aAgregar)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) VALUES(@cod,@nom,@desc,@idMarca,@idCat,@img,@precio)");
                datos.setearParametro("@cod", aAgregar.Codigo);
                datos.setearParametro("@nom", aAgregar.Nombre);
                datos.setearParametro("@desc", aAgregar.Descripcion);
                datos.setearParametro("@idMarca", aAgregar.Marca.Id);
                datos.setearParametro("@idCat", aAgregar.Categoria.Id);
                datos.setearParametro("@img", aAgregar.Imagen ?? (object)DBNull.Value);
                datos.setearParametro("@precio", aAgregar.Precio);
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



        public void modificarArticulo(Articulo aModificar)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("update ARTICULOS set Codigo=@cod,Nombre=@nom,Descripcion=@desc,IdMarca=@idMarca,IdCategoria=@idCat,ImagenUrl=@img,Precio=@precio where Id=" + aModificar.Id);
                datos.setearParametro("@cod", aModificar.Codigo);
                datos.setearParametro("@nom", aModificar.Nombre);
                datos.setearParametro("@desc", aModificar.Descripcion);
                datos.setearParametro("@idMarca", aModificar.Marca.Id);
                datos.setearParametro("@idCat", aModificar.Categoria.Id);
                datos.setearParametro("@img", aModificar.Imagen ?? (object)DBNull.Value);
                datos.setearParametro("@precio", aModificar.Precio);
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


        public void eliminarArticulo(int id)
        {
            try
            {
                datos = new AccesoADatos();
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id=" + id);
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

        public List<Articulo> filtrar(string campo, string criterio, string filtro, string categoria, string marca)
        {
            datos = new AccesoADatos();

            string consulta = "SELECT A.Id,Codigo,Nombre,A.Descripcion,Precio,M.Descripcion as Marca,C.Descripcion as Categoria,ImagenUrl,A.IdMarca,A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C WHERE A.IdMarca = M.Id and A.IdCategoria = C.Id ";

            switch (categoria)
            {
                case "Celulares":
                    consulta += " AND A.IdCategoria=1";
                    break;

                case "Televisores":
                    consulta += " AND A.IdCategoria=2";
                    break;
                case "Media":
                    consulta += " AND A.IdCategoria=3";
                    break;
                case "Audio":
                    consulta += " AND A.IdCategoria=4";
                    break;
                default:
                    break;
            }

            switch (marca)
            {
                case "Samsung":
                    consulta += " AND A.IdMarca=1";
                    break;

                case "Apple":
                    consulta += " AND A.IdMarca=2";
                    break;
                case "Sony":
                    consulta += " AND A.IdMarca=3";
                    break;
                case "Huawei":
                    consulta += " AND A.IdMarca=4";
                    break;
                case "Motorola":
                    consulta += " AND A.IdMarca=5";
                    break;
                default:
                    break;
            }

            if (campo == "Precio")
            {
                switch (criterio)
                {
                    case "Igual a":
                        consulta += "AND Precio = " + filtro;
                        break;

                    case "Menor a":
                        consulta += "AND Precio < " + filtro;
                        break;

                    default:
                        consulta += "AND Precio > " + filtro;
                        break;
                }
            }

            else
            {
                campo = campo == "Nombre" ? "Nombre" : "A.Descripcion";
                switch (criterio)
                {
                    case "Empieza con":
                        consulta += "AND " + campo + " like '" + filtro + "%'";
                        break;

                    case "Termina con":
                        consulta += "AND " + campo + " like '%" + filtro + "'";
                        break;

                    default:
                        consulta += "AND " + campo + " like '%" + filtro + "%'";
                        break;
                }
            }

            try
            {
                return traerArticulos(consulta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Articulo> traerArticulos(string consulta)
        {

            try
            {
                List<Articulo> listaDeArticulos = new List<Articulo>();
                datos = new AccesoADatos();
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                SqlDataReader lector = datos.obtenerLector();
                while (lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)lector["Id"];
                    articulo.Codigo = (string)lector["Codigo"];
                    articulo.Nombre = (string)lector["Nombre"];
                    articulo.Descripcion = (string)lector["Descripcion"];
                    articulo.Marca = new Elemento();
                    articulo.Marca.Descripcion = (string)lector["Marca"];
                    articulo.Marca.Id = (int)lector["IdMarca"];
                    articulo.Categoria = new Elemento();
                    articulo.Categoria.Id = (int)lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)lector["Categoria"];
                    if (!(lector["ImagenUrl"] is DBNull))
                        articulo.Imagen = (string)lector["ImagenUrl"];
                    articulo.Precio = (decimal)lector["Precio"];
                    listaDeArticulos.Add(articulo);
                }

                return listaDeArticulos;
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
