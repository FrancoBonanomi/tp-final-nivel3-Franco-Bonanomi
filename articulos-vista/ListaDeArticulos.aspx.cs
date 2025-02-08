using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.Data.SqlClient;

namespace articulos_vista
{
    public partial class ListaDeArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mensajeError.Visible = false;
            try
            {
                if (Session["logueado"] != null && !((Usuario)Session["logueado"]).Admin)
                    contDenegarAcceso.Visible = true;
                if (!IsPostBack)
                {
                    Session.Remove("listaFiltrados");
                    agregarOpciones(ddlCampo, "Nombre", "Descripción", "Precio");
                    agregarOpciones(ddlCriterio, "Empieza con", "Termina con", "Contiene");
                    agregarOpciones(ddlMarca, "Samsung", "Apple", "Sony", "Huawei", "Motorola", "Todos");
                    agregarOpciones(ddlCategoria, "Celulares", "Televisores", "Media", "Audio", "Todos");
                }
                if (Session["listaFiltrados"] == null)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    dgvArticulos.DataSource = negocio.listarArticulos();
                    dgvArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }

        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        private void agregarOpciones(DropDownList desplegable, string opcion1, string opcion2, string opcion3, string opcion4 = "", string opcion5 = "", string opcion6 = "")
        {
            desplegable.Items.Add(opcion1);
            desplegable.Items.Add(opcion2);
            desplegable.Items.Add(opcion3);
            if (opcion4 != "")
                desplegable.Items.Add(opcion4);
            if (opcion5 != "")
                desplegable.Items.Add(opcion5);
            if (opcion6 != "")
                desplegable.Items.Add(opcion6);
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedValue == "Precio")
                agregarOpciones(ddlCriterio, "Menor a", "Mayor a", "Igual a");

            else
                agregarOpciones(ddlCriterio, "Empieza con", "Termina con", "Contiene");
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["listaFiltrados"] != null)
                dgvArticulos.DataSource = Session["listaFiltrados"];
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCampo.SelectedValue == "Precio")
                {
                    if (Validaciones.validarVacio(txtFiltro.Text))
                    {
                        Validaciones.mostrarMensajeError(mensajeError, "Al filtrar por número, el filtro no debe estar vacío");
                        return;
                    }
                }
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaFiltrados", negocio.filtrar(ddlCampo.SelectedValue, ddlCriterio.SelectedValue, txtFiltro.Text, ddlCategoria.SelectedValue, ddlMarca.SelectedValue));
                dgvArticulos.DataSource = Session["listaFiltrados"];
                dgvArticulos.DataBind();
            }

            catch (SqlException ex)
            {
                Validaciones.mostrarMensajeError(mensajeError, "El valor ingresado en el campo número es inválido");
            }

            catch (Exception ex)
            {
                Validaciones.mostrarMensajeError(mensajeError);
            }
        }
    }
}