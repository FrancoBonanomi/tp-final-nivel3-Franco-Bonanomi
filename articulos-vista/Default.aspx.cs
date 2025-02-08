using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.IO;

namespace articulos_vista
{
    public partial class Default : System.Web.UI.Page
    {

        private List<Articulo> listaDeArticulos;

        protected void Page_Load(object sender, EventArgs e)
        {
            mensajeError.Visible = false;
            mensajeExito.Visible = false;
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> listaFavoritos = new List<Articulo>();
                if (Session["logueado"] != null)
                    listaFavoritos = negocio.listarFavoritos(((Usuario)Session["logueado"]).Id);
                listaDeArticulos = negocio.listarArticulos();
                string ruta = Server.MapPath("./imagenes-articulos/");
                foreach (Articulo articulo in listaDeArticulos)
                {
                    if (articulo.Descripcion == null || articulo.Descripcion.Replace(" ", "") == "")
                        articulo.Descripcion = "Sin descripción";

                    if (File.Exists(ruta + articulo.Imagen))
                        articulo.Imagen = "./imagenes-articulos/" + articulo.Imagen;

                    else if (articulo.Imagen == null || !articulo.Imagen.ToLower().Contains("http"))
                        articulo.Imagen = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";

                    foreach (Articulo favorito in listaFavoritos)
                    {
                        if (articulo.Id == favorito.Id)
                        {
                            articulo.Accion = "Quitar de favoritos";
                            articulo.Clase = "btn btn-danger mb-1";
                        }
                    }
                }
                if (!IsPostBack)
                {
                    repetidor.DataSource = listaDeArticulos;
                    repetidor.DataBind();
                }
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }

        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            listaDeArticulos = listaDeArticulos.FindAll(x => contieneFiltro(x.Nombre) || contieneFiltro(x.Descripcion));
            repetidor.DataSource = listaDeArticulos;
            repetidor.DataBind();
        }

        private bool contieneFiltro(string valor)
        {
            return valor.ToLower().Contains(txtFiltro.Text.ToLower());
        }


        protected void btnFavoritos_Click1(object sender, EventArgs e)
        {
            try
            {
                if (Session["logueado"] == null)
                {
                    Validaciones.mostrarMensajeError(mensajeError, "Debes estar logueado para agregar artículos a favoritos");
                    return;
                }
                ArticuloNegocio negocio = new ArticuloNegocio();
                Button boton = (Button)sender;
                int idUsuario = ((Usuario)Session["logueado"]).Id;
                int idArticulo = int.Parse(boton.CommandArgument);
                if (boton.Text == "Agregar a favoritos")
                {
                    negocio.agregarAFavoritos(idUsuario, idArticulo);
                    Validaciones.mostrarMensajeError(mensajeExito, "Artículo agregado exitosamente a favoritos");
                    boton.Text = "Quitar de favoritos";
                    boton.CssClass = "btn btn-danger mb-1";
                }
                else
                {
                    negocio.quitarDeFavoritos(idArticulo, idUsuario);
                    Validaciones.mostrarMensajeError(mensajeExito, "Artículo eliminado exitosamente de favoritos");
                    boton.Text = "Agregar a favoritos";
                    boton.CssClass = "btn btn-primary mb-1";
                }
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }
    }
}