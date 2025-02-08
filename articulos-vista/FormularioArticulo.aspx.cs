using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace articulos_vista
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mensajeError.Visible = false;
                mensajeExito.Visible = false;
                txtNombre.CssClass = "form-control";
                txtCodigo.CssClass = "form-control";
                txtPrecio.CssClass = "form-control";

                if (!IsPostBack)
                {
                    precargarDesplegables(ddlMarca, "SELECT Id,Descripcion from MARCAS");
                    precargarDesplegables(ddlCategoria, "SELECT Id,Descripcion from CATEGORIAS");
                }

                if (Request.QueryString["id"] != null)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    List<Articulo> listaDeArticulos = negocio.listarArticulos();
                    Articulo encontrado = listaDeArticulos.Find(x => x.Id == int.Parse(Request.QueryString["id"]));
                    if (!IsPostBack)
                    {
                        txtCodigo.Text = encontrado.Codigo;
                        txtNombre.Text = encontrado.Nombre;
                        txtDescripcion.Text = encontrado.Descripcion;
                        ddlMarca.SelectedValue = encontrado.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = encontrado.Categoria.Id.ToString();
                        txtPrecio.Text = encontrado.Precio.ToString();
                        string ruta = Server.MapPath("./imagenes-articulos/");

                        if (File.Exists(ruta + encontrado.Imagen))
                        {
                            imgArticulo.ImageUrl = "./imagenes-articulos/" + encontrado.Imagen;
                            btnQuitarImagen.Visible = true;

                        }
                        else if (encontrado.Imagen != null)
                        {
                            if (encontrado.Imagen.ToLower().Contains("http"))
                            {
                                imgArticulo.ImageUrl = encontrado.Imagen;
                                btnQuitarImagen.Visible = true;
                            }
                        }
                        btnAgregar.Text = "Modificar";
                        btnEliminar.Visible = true;

                    }
                    Session.Add("imagen", encontrado.Imagen);

                }

                else
                    Session.Remove("imagen");
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;
                if (Validaciones.validarVacio(txtCodigo.Text) || Validaciones.validarVacio(txtNombre.Text) || Validaciones.validarVacio(txtPrecio.Text))
                {
                    if (Validaciones.validarVacio(txtCodigo.Text))
                        txtCodigo.CssClass = "form-control is-invalid";
                    else
                        txtCodigo.CssClass = "form-control is-valid";
                    if (Validaciones.validarVacio(txtNombre.Text))
                        txtNombre.CssClass = "form-control is-invalid";
                    else
                        txtNombre.CssClass = "form-control is-valid";
                    if (Validaciones.validarVacio(txtPrecio.Text))
                        txtPrecio.CssClass = "form-control is-invalid";
                    else
                        txtPrecio.CssClass = "form-control is-valid";

                    Validaciones.mostrarMensajeError(mensajeError, "Porfavor, Complete todos los campos requeridos");
                    return;
                }

                txtCodigo.CssClass = "form-control is-valid";
                txtNombre.CssClass = "form-control is-valid";
                txtPrecio.CssClass = "form-control is-valid";

                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();
                nuevo.Codigo = txtCodigo.Text.ToUpper();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Marca = new Elemento();
                nuevo.Categoria = new Elemento();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedItem.Value);
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedItem.Value);
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Imagen = Session["imagen"] != null ? Session["imagen"].ToString() : null;
                string ruta = Server.MapPath("./imagenes-articulos/");
                if (subirImgArticulo.HasFile)
                {
                    if (Validaciones.validarImagen(subirImgArticulo.PostedFile.FileName))
                    {
                        if (File.Exists(ruta + nuevo.Imagen))
                            File.Delete(ruta + nuevo.Imagen);
                        string nombreArchivoSubido = subirImgArticulo.PostedFile.FileName;
                        string fechaSinBarra = DateTime.Now.ToString().Replace("/", "");
                        fechaSinBarra = fechaSinBarra.Replace(":", "");
                        subirImgArticulo.PostedFile.SaveAs(ruta + fechaSinBarra + "-" + nombreArchivoSubido);
                        nuevo.Imagen = fechaSinBarra + "-" + nombreArchivoSubido;
                        imgArticulo.ImageUrl = "./imagenes-articulos/" + fechaSinBarra + "-" + nombreArchivoSubido;
                        btnQuitarImagen.Visible = true;
                    }
                    else
                    {
                        Validaciones.mostrarMensajeError(mensajeError, "El archivo seleccionado no es una imagen");
                        return;
                    }
                }

                else
                {
                    if (btnAgregar.Text == "Agregar")
                        imgArticulo.ImageUrl = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
                }
                if (btnAgregar.Text == "Agregar")
                {
                    negocio.agregarArticulo(nuevo);
                    mensajeExito.Visible = true;
                    mensajeExito.InnerText = "Articulo agregado exitosamente";
                }
                else
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificarArticulo(nuevo);
                    mensajeExito.Visible = true;
                    mensajeExito.InnerText = "Articulo Modificado exitosamente";
                }
            }
            catch (FormatException ex)
            {
                txtPrecio.CssClass = "form-control is-invalid";
                Validaciones.mostrarMensajeError(mensajeError, "El precio ingresado es inválido");
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }

        private void precargarDesplegables(DropDownList desplegable, string consulta)
        {
            ElementoNegocio negocio = new ElementoNegocio();
            desplegable.DataSource = negocio.listarElementos(consulta);
            desplegable.DataValueField = "Id";
            desplegable.DataTextField = "Descripcion";
            desplegable.DataBind();
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Eliminar")
                confirmarEliminacionContenedor.Visible = true;
            else
                confirmarEliminacionImg.Visible = true;
        }

        protected void btnConfirmarElim_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckbConfirmarElim.Checked)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminarArticulo(int.Parse(Request.QueryString["id"]));
                    string ruta = Server.MapPath("./imagenes-articulos/");
                    if (Session["imagen"] != null && File.Exists(ruta + Session["imagen"].ToString()))
                        File.Delete(ruta + Session["imagen"].ToString());
                    negocio.quitarDeFavoritos(int.Parse(Request.QueryString["id"]));
                    Response.Redirect("ListaDeArticulos.aspx");
                }
                else
                    confirmarEliminacionContenedor.Visible = false;

            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError, "Ocurrió un error al eliminar el artículo, intente de nuevo");
            }
        }

        protected void btnConfirmarElimImg_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckbEliminarImagen.Checked)
                {
                    string ruta = Server.MapPath("./imagenes-articulos/");
                    if (File.Exists(ruta + Session["imagen"]))
                        File.Delete(ruta + Session["imagen"]);
                    mensajeExito.Visible = true;
                    mensajeExito.InnerText = "Imagen eliminada exitosamente";
                    imgArticulo.ImageUrl = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.quitarImagen(int.Parse(Request.QueryString["id"]));
                    btnQuitarImagen.Visible = false;
                }
                confirmarEliminacionImg.Visible = false;

            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError, "Ocurrió un error al eliminar la imagen, intente de nuevo");
            }
        }
    }
}