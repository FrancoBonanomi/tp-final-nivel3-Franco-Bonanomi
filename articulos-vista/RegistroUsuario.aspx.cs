using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace articulos_vista
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mensajeError.Visible = false;
            if (Request.QueryString["accion"] == "login")
            {
                datoApellido.Visible = false;
                datoNombre.Visible = false;
                btnRegistro.Text = "Ingresar";
                btnRegistro.CssClass = "btn btn-success";
                string pagina = Request.QueryString["pagina"];
                titulo.InnerHtml = pagina != null ? "Debes tener una cuenta para ingresar a: <span class='text-primary'>" + pagina + "</span>" : "Ingresar al sistema";
                btnSinCuenta.Visible = pagina != null;
            }
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;
                if (btnRegistro.Text == "Registrarse")
                {
                    if (Validaciones.validarVacio(txtNombre.Text) || Validaciones.validarVacio(txtApellido.Text))
                    {
                        Validaciones.mostrarMensajeError(mensajeError, "Complete todos los campos requeridos");
                        return;
                    }
                }
                if (Validaciones.validarVacio(txtEmail.Text) || Validaciones.validarVacio(txtPass.Text))
                {
                    Validaciones.mostrarMensajeError(mensajeError, "Complete todos los campos requeridos");
                    return;
                }
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPass.Text;
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                if (btnRegistro.Text == "Registrarse")
                {
                    if (negocio.loguearUsuario(usuario))
                    {
                        Validaciones.mostrarMensajeError(mensajeError, "El email proporcionado ya esta en uso");
                        return;
                    }

                    int id = negocio.registrarUsuario(usuario);
                    usuario.Id = id;
                    Session.Add("logueado", usuario);
                    Response.Redirect("MiPerfil.aspx");
                }
                else
                {
                    if (negocio.loguearUsuario(usuario, "and pass=@pass"))
                    {
                        Session.Add("logueado", usuario);
                        Response.Redirect("MiPerfil.aspx");
                    }

                    else
                        Validaciones.mostrarMensajeError(mensajeError, "Email y/o contraseña incorrectos");
                }

            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }
    }
}