<%@ Page Title="Detalle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="UniCampus.Web.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <a href="Actividades.aspx">&larr; Volver</a>
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" />

    <div class="rejilla">
        <div class="panel">
            <h2>Detalles de la Actividad</h2>
            <table class="detalle">
                <tr><th>Título:</th><td><asp:Label ID="lblTitulo" runat="server" /></td></tr>
                <tr><th>Tipo:</th><td><asp:Label ID="lblTipo" runat="server" /></td></tr>
                <tr><th>Descripción:</th><td><asp:Label ID="lblDescripcion" runat="server" /></td></tr>
                <tr><th>Fecha:</th><td><asp:Label ID="lblFecha" runat="server" /></td></tr>
                <tr><th>Hora:</th><td><asp:Label ID="lblHora" runat="server" /></td></tr>
                <tr><th>Modalidad:</th><td><asp:Label ID="lblModalidad" runat="server" /></td></tr>
                <tr><th>Lugar:</th><td><asp:Label ID="lblLugar" runat="server" /></td></tr>
                <tr><th>Cupos:</th><td><asp:Label ID="lblCupos" runat="server" /></td></tr>
                <tr><th>Estado:</th><td><asp:Label ID="lblEstado" runat="server" /></td></tr>
            </table>
            <br />
            <asp:Button ID="btnInscribirme" runat="server" Text="Inscribirme" CssClass="btn btn-azul" OnClick="btnInscribirme_Click" />
        </div>

        <div class="panel">
            <h2>Estudiantes Inscritos</h2>
            <asp:GridView ID="gvInscritos" runat="server" AutoGenerateColumns="False" CssClass="tabla" GridLines="None"
                EmptyDataText="No hay inscritos.">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Programa" HeaderText="Programa" />
                    <asp:BoundField DataField="FechaInscripcion" HeaderText="Fecha inscripción" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="panel">
        <h2>Reseñas</h2>
        <div class="fila">
            <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="campo">
                <asp:ListItem Value="5">5 - Excelente</asp:ListItem>
                <asp:ListItem Value="4">4 - Muy buena</asp:ListItem>
                <asp:ListItem Value="3">3 - Buena</asp:ListItem>
                <asp:ListItem Value="2">2 - Regular</asp:ListItem>
                <asp:ListItem Value="1">1 - Mala</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtComentario" runat="server" CssClass="campo" placeholder="Escribe un comentario" />
            <asp:Button ID="btnPublicar" runat="server" Text="Publicar reseña" CssClass="btn btn-azul" OnClick="btnPublicar_Click" />
        </div>
        <asp:GridView ID="gvResenas" runat="server" AutoGenerateColumns="False" CssClass="tabla" GridLines="None"
            EmptyDataText="Todavía no hay reseñas.">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Estudiante" />
                <asp:BoundField DataField="Calificacion" HeaderText="Calificación" />
                <asp:BoundField DataField="Comentario" HeaderText="Comentario" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
