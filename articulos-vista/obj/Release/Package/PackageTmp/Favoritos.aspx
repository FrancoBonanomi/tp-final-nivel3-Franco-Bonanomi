<%@ Page Title="Favoritos" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="articulos_vista.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Favoritos</h1>
    <div class="row row-cols-1 row-cols-md-3 g-4">


        <asp:Repeater ID="repetidor" runat="server">
            <ItemTemplate>

                <div class="col">
                    <div class="card h-100 text-bg-light position-relative">
                        <img src="<%#Eval("Imagen")%>"
                            class="card-img-top mw-100 imagen-articulo object-fit-contain" alt="imagen-articulo">
                        <div class="card-body">
                            <img class="position-absolute estrella" src="https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEhAdazsuOkLbHmK_AaKeSyQLM3Bp2PF6gxNdWh4ziciJfQIK5gMhr7bpzcFrwt4IxcCVjisyodOGDJyS8UZL4mCdFcueIF20OosGngvj0gQZw_52r9Yg3_UfawrlfwAXi_hpKcvDOrX0ok/s1600/MC900441361%5B1%5D.png" />
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion")%></p>
                            <!-- Button trigger modal -->
                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal<%#Eval("Id") %>">
                                Ver detalle
                            </button>

                            <!-- Modal -->

                        </div>
                    </div>
                    <div class="modal fade" id="exampleModal<%#Eval("Id") %>" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel"><%#Eval("Nombre")%></h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div>
                                        <img src="<%#Eval("Imagen") %>" alt="imagen-artículo" class="mx-auto d-block mw-100 object-fit-contain imagen-articulo" />
                                    </div>
                                    <span class="fw-bold text-primary">CÓDIGO:</span>
                                    <p>
                                        <%#Eval("Codigo")%>
                                    </p>
                                    <span class="fw-bold text-primary">DESCRIPCIÓN:</span>
                                    <p>
                                        <%#Eval("Descripcion") %>
                                    </p>
                                    <span class="fw-bold text-primary">PRECIO:</span>
                                    <p>
                                        $<%#Eval("Precio") %>
                                    </p>
                                    <span class="fw-bold text-primary">MARCA:</span>
                                    <p>
                                        <%#Eval("Marca.Descripcion")%>
                                    </p>
                                    <span class="fw-bold text-primary">CATEGORÍA:</span>
                                    <p>
                                        <%#Eval("Categoria.Descripcion")%>
                                    </p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </ItemTemplate>
        </asp:Repeater>


    </div>
</asp:Content>
