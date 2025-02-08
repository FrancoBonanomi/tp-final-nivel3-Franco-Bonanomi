<%@ Page Title="Listado" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaDeArticulos.aspx.cs" Inherits="articulos_vista.ListaDeArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Listado de artículos</h1>
    <div class="vw-100 vh-100 denegar-acceso position-fixed top-0 start-0" visible="false" runat="server" id="contDenegarAcceso">
        <div class="alert alert-danger position-absolute top-50 start-50 translate-middle" role="alert">
            Es necesario tener perfil admin para acceder a esta pantalla. <a href="RegistroUsuario.aspx?accion=login" class="alert-link">Loguearse con perfil admin</a>.
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-lg-4">
            <asp:Label ID="lblCampo" runat="server" Text="Campo:" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-lg-4">
            <asp:Label ID="lblCriterio" runat="server" Text="Criterio:" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-lg-4">
            <asp:Label ID="lblFiltro" runat="server" Text="Filtro:" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtFiltro" type="search" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

    </div>
    <div class="row mb-5">
        <div class="col-lg-4 columna">
            <asp:Label ID="lblCategoria" runat="server" Text="Categoría:" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
            <asp:Button ID="btnBuscar" runat="server" Text="Filtrar" CssClass="btn btn-primary mt-2 boton-buscar" OnClick="btnBuscar_Click" />
        </div>
        <div class="col-lg-4">
            <asp:Label ID="lblMarca" runat="server" Text="Marca:" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <asp:GridView runat="server" ID="dgvArticulos" DataKeyNames="Id" CssClass="table table-bordered d-lg-table d-block overflow-x-auto" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" AllowPaging="true" PageSize="5" OnPageIndexChanging="dgvArticulos_PageIndexChanging">
        <Columns>
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:CommandField HeaderText="Acción" SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
    </asp:GridView>
    <a href="FormularioArticulo.aspx">Agregar</a>
    <div class="alert alert-danger mt-2" role="alert" runat="server" id="mensajeError">
        ocurrió un error, intente de nuevo
    </div>
    <%--<div class="mt-5">
        <asp:RequiredFieldValidator ErrorMessage="Al filtrar por número, el filtro no puede estar vacio" ControlToValidate="txtFiltro" runat="server" CssClass="alert alert-danger mt-2" />
    </div>--%>
</asp:Content>
