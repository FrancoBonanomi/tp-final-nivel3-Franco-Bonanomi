<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="articulos_vista.RegistroUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 id="titulo" runat="server">Registrarse en el sistema</h1>
    <div class="row">
        <div class="col-md-8 col-lg-7 col-xl-6">
            <div class="mb-3" runat="server" id="datoNombre">
                <asp:Label runat="server" ID="lblNombre" Text="Nombre(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"></asp:TextBox>
            </div>
            <div class="mb-3" runat="server" id="datoApellido">
                <asp:Label runat="server" ID="lblApellido" Text="Apellido(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" ID="lblEmail" Text="Email(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>

            </div>
            <div class="mb-3">
                <asp:Label runat="server" ID="lblPass" Text="Contraseña(*):" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPass" type="password"></asp:TextBox>

            </div>
            <asp:Button runat="server" Text="Registrarse" ID="btnRegistro" OnClick="btnRegistro_Click" CssClass="btn btn-primary" />

            <a href="RegistroUsuario.aspx" runat="server" text="No tengo cuenta" visible="false" id="btnSinCuenta" class="btn btn-info">No tengo cuenta</a>

            <div class="alert alert-danger mt-2" role="alert" runat="server" id="mensajeError">
                El archivo seleccionado no es una imagen
            </div>

            <div class="mt-5">
                <asp:RegularExpressionValidator ErrorMessage="Email inválido" ControlToValidate="txtEmail" runat="server" CssClass="alert alert-danger" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
            </div>
            <div class="mt-5">
                <asp:RegularExpressionValidator ErrorMessage="Nombre inválido" ControlToValidate="txtNombre" runat="server" CssClass="alert alert-danger" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:[-' ][A-Za-zÁÉÍÓÚáéíóúÑñ]+)*$" />
            </div>
            <div class="mt-5">
                <asp:RegularExpressionValidator ErrorMessage="Apellido inválido" ControlToValidate="txtApellido" runat="server" CssClass="alert alert-danger" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:[-' ][A-Za-zÁÉÍÓÚáéíóúÑñ]+)*$" />
            </div>
        </div>
    </div>
</asp:Content>
