<%@ Page Title="Mi perfil" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="articulos_vista.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Mi perfil</h1>
    <div class="row">
        <div class="col-md-7 col-lg-6">
            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

            </div>
            <div class="mb-3">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>

            </div>
            <div class="mb-3">
                <asp:Label ID="lblImagen" runat="server" Text="Imagen:" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="subirImagen" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary mb-1" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnEliminarFoto" Visible="false" runat="server" Text="Eliminar foto de perfil" CssClass="btn btn-danger mb-1" OnClick="btnEliminarFoto_Click1" />
            <div id="confirmarEliminacionContenedor" runat="server" visible="false" class="my-2">
                <input type="checkbox" runat="server" class="form-check-input align-middle" id="ckbConfirmarElim" />
                <asp:Label ID="lblConfirmarEliminacion" runat="server" Text="Confirmar eliminación?" CssClass="form-label align-middle"></asp:Label>
                <asp:Button ID="btnConfirmarElim" runat="server" Text="Confirmar" CssClass="btn btn-outline-danger" OnClick="btnConfirmarElim_Click" />
            </div>
            <div class="alert alert-danger mt-2" role="alert" runat="server" id="mensajeError">
                ocurrió un error, intente de nuevo
            </div>
            <div class="alert alert-success mt-2" role="alert" runat="server" id="mensajeExito">
                Usuario modificado exitosamente
            </div>
            <div class="mt-5">
                <asp:RegularExpressionValidator CssClass="alert alert-danger" ErrorMessage="Nombre inválido" ControlToValidate="txtNombre" runat="server" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:[-' ][A-Za-zÁÉÍÓÚáéíóúÑñ]+)*$" />
            </div>
            <div class="mt-5">
                <asp:RegularExpressionValidator CssClass="alert alert-danger" ErrorMessage="Apellido inválido" ControlToValidate="txtApellido" runat="server" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:[-' ][A-Za-zÁÉÍÓÚáéíóúÑñ]+)*$" />
            </div>
        </div>
        <div class="col-md-5 col-lg-4 d-flex align-items-center">
            <div class="mb-3">
                <asp:Image ID="imgPerfil" runat="server" AlternateText="foto de perfil" ImageUrl="https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png" CssClass="mw-100" />
            </div>
        </div>
    </div>

</asp:Content>
