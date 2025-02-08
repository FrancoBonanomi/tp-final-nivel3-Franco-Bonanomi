<%@ Page Title="Formulario" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="articulos_vista.FormularioPokemon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-7">
            <div class="mb-3">
                <asp:Label ID="lblCodigo" runat="server" Text="Código (letra número número)(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblMarca" runat="server" Text="Marca:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCategoria" runat="server" Text="Categoría:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblImagen" runat="server" Text="Subir imagen:" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="subirImgArticulo" runat="server" CssClass="form-control" />
                <%-- <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>--%>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblPrecio" runat="server" Text="Precio(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
            <asp:Button ID="btnQuitarImagen" runat="server" Text="Eliminar imagen" CssClass="btn btn-warning boton-eliminarImg" Visible="false" onclick="btnEliminar_Click"/>
            <a class="volver-a-lista" href="ListaDeArticulos.aspx">Volver a la lista de artículos</a>
            <div id="confirmarEliminacionContenedor" class="my-2" visible="false" runat="server">
                <input type="checkbox" runat="server" class="form-check-input align-middle" id="ckbConfirmarElim" />
                <asp:Label ID="lblConfirmarEliminacion" runat="server" Text="Confirmar eliminación?" CssClass="form-label align-middle"></asp:Label>
                <asp:Button ID="btnConfirmarElim" runat="server" Text="Confirmar" CssClass="btn btn-outline-danger" OnClick="btnConfirmarElim_Click" />
            </div>
            <div id="confirmarEliminacionImg" class="my-2" visible="false" runat="server">
                <input type="checkbox" runat="server" class="form-check-input align-middle" id="ckbEliminarImagen" />
                <asp:Label ID="lblEliminarImagen" runat="server" Text="Confirmar eliminación de imagen?" CssClass="form-label align-middle"></asp:Label>
                <asp:Button ID="btnConfirmarElimImg" runat="server" Text="Confirmar" CssClass="btn btn-outline-warning" onclick="btnConfirmarElimImg_Click"/>
            </div>
            <div class="alert alert-danger mt-2" role="alert" runat="server" id="mensajeError">
                El archivo seleccionado no es una imagen
            </div>
            <div class="alert alert-success mt-2" role="alert" runat="server" id="mensajeExito">
                Artículo agregado exitosamente
            </div>
            <div class="mt-5">
                <asp:RegularExpressionValidator ErrorMessage="Código invalido" ControlToValidate="txtCodigo" runat="server" CssClass="alert alert-danger mt-2" ValidationExpression="^[A-Za-z]\d{2}$" />
            </div>
        </div>
        <div class="col-md-4 d-flex align-items-center">
            <asp:Image ID="imgArticulo" runat="server" AlternateText="imagen-artículo" CssClass="mw-100" ImageUrl="https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png" />
        </div>
    </div>
</asp:Content>
