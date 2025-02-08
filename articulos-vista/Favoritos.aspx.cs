using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace articulos_vista
{
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logueado"] == null)
                return;
            int idUsuario = ((Usuario)Session["logueado"]).Id;
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> listaDeFavoritos = negocio.listarFavoritos(idUsuario);
            string ruta = Server.MapPath("./imagenes-articulos/");
            foreach (Articulo articulo in listaDeFavoritos)
            {
                if (articulo.Descripcion == null || articulo.Descripcion.Replace(" ", "") == "")
                    articulo.Descripcion = "Sin descripción";

                if (File.Exists(ruta + articulo.Imagen))
                    articulo.Imagen = "./imagenes-articulos/" + articulo.Imagen;

                else if (articulo.Imagen == null || !articulo.Imagen.ToLower().Contains("http"))
                    articulo.Imagen = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
            }
            repetidor.DataSource = listaDeFavoritos;
            repetidor.DataBind();
        }
    }
}