using dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace articulos_vista
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page is Default || Page is RegistroUsuario))
            {
                if (Session["logueado"] == null)
                    Response.Redirect("RegistroUsuario.aspx?accion=login&pagina=" + Page.Title);
            }
            if (Session["logueado"] != null)
            {
                Usuario usuario = (Usuario)Session["logueado"];
                lblEmail.Text = usuario.Email;
                btnLogin.Visible = false;
                btnRegistrarse.Visible = false;
                btnSalir.Visible = true;

                if (usuario.Imagen == null)
                    return;
                string ruta = Server.MapPath("./imagenes-usuarios/");
                if (File.Exists(ruta + usuario.Imagen))
                    imgPerfil.ImageUrl = "./imagenes-usuarios/" + usuario.Imagen;
                else if (usuario.Imagen.ToLower().Contains("http"))
                    imgPerfil.ImageUrl = usuario.Imagen;
            }
        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Registrarse")
                Response.Redirect("RegistroUsuario.aspx");
            else if (((Button)sender).Text == "Login")
                Response.Redirect("RegistroUsuario.aspx?accion=login");
            else
            {
                Session.Remove("logueado");
                Response.Redirect("Default.aspx");
            }

        }
    }
}