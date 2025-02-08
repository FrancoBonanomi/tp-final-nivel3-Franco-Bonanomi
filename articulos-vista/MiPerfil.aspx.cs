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
    public partial class MiPerfil : System.Web.UI.Page
    {

        private Usuario usuario;
        private string ruta;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mensajeError.Visible = false;
                mensajeExito.Visible = false;
                txtNombre.CssClass = "form-control";
                txtApellido.CssClass = "form-control";
                txtEmail.CssClass = "form-control";

                usuario = (Usuario)Session["logueado"];
                ruta = Server.MapPath("./imagenes-usuarios/");
                if (usuario == null)
                    return;
                if (!IsPostBack)
                {
                    txtEmail.Text = usuario.Email;
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    if (File.Exists(ruta + usuario.Imagen))
                    {
                        imgPerfil.ImageUrl = "./imagenes-usuarios/" + usuario.Imagen;
                        btnEliminarFoto.Visible = true;
                    }
                    else if (usuario.Imagen != null && usuario.Imagen.ToLower().Contains("http"))
                    {
                        imgPerfil.ImageUrl = usuario.Imagen;
                        btnEliminarFoto.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                txtEmail.CssClass = "form-control is-valid";

                if (Validaciones.validarVacio(txtNombre.Text) || Validaciones.validarVacio(txtApellido.Text))
                {
                    if (Validaciones.validarVacio(txtNombre.Text))
                        txtNombre.CssClass = "form-control is-invalid";
                    else
                        txtNombre.CssClass = "form-control is-valid";

                    if (Validaciones.validarVacio(txtApellido.Text))
                        txtApellido.CssClass = "form-control is-invalid";
                    else
                        txtApellido.CssClass = "form-control is-valid";

                    Validaciones.mostrarMensajeError(mensajeError, "Complete todos los campos requeridos");
                    return;
                }


                txtNombre.CssClass = "form-control is-valid";
                txtApellido.CssClass = "form-control is-valid";
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                if (subirImagen.HasFile)
                {
                    if (Validaciones.validarImagen(subirImagen.PostedFile.FileName))
                    {
                        if (File.Exists(ruta + usuario.Imagen))
                            File.Delete(ruta + usuario.Imagen);
                        string nombreArchivoSubido = subirImagen.PostedFile.FileName;
                        string fechaSinBarra = DateTime.Now.ToString().Replace("/", "");
                        fechaSinBarra = fechaSinBarra.Replace(":", "");
                        subirImagen.PostedFile.SaveAs(ruta + fechaSinBarra + "-" + nombreArchivoSubido);
                        usuario.Imagen = fechaSinBarra + "-" + nombreArchivoSubido;
                        imgPerfil.ImageUrl = "./imagenes-usuarios/" + usuario.Imagen;
                        ((Image)Master.FindControl("imgPerfil")).ImageUrl = "./imagenes-usuarios/" + usuario.Imagen;
                        btnEliminarFoto.Visible = true;
                    }
                    else
                    {
                        Validaciones.mostrarMensajeError(mensajeError, "El archivo seleccionado no es una imagen");
                        return;
                    }

                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.actualizarUsuario(usuario);
                mensajeExito.Visible = true;
                mensajeExito.InnerText = "Usuario modificado exitosamente";
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }

        }
        protected void btnEliminarFoto_Click1(object sender, EventArgs e)
        {
            confirmarEliminacionContenedor.Visible = true;
        }


        protected void btnConfirmarElim_Click(object sender, EventArgs e)
        {
            try
            {

                if (ckbConfirmarElim.Checked)
                {

                    if (File.Exists(ruta + usuario.Imagen))
                        File.Delete(ruta + usuario.Imagen);
                    usuario.Imagen = null;
                    mensajeExito.Visible = true;
                    mensajeExito.InnerText = "Imagen eliminada exitosamente";
                    btnEliminarFoto.Visible = false;
                    imgPerfil.ImageUrl = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
                    ((Image)Master.FindControl("imgPerfil")).ImageUrl = "https://png.pngtree.com/png-vector/20220608/ourmid/pngtree-anonymous-user-unidentified-contact-avatar-png-image_4816655.png";
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.quitarImagen(usuario.Id);
                }
                confirmarEliminacionContenedor.Visible = false;
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }

    }
}





