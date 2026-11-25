<%@ Page Title="Actividades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Actividades.aspx.cs" Inherits="UniCampus.Web.Actividades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Registro de Actividades Académicas</h1>
    <p>Administra, registra y consulta las actividades académicas de la institución.</p>

    <asp:Label ID="lblMensaje" runat="server" />

    <div class="rejilla">
        <div class="panel">
            <h2>Nueva Actividad Académica</h2>

            <label>Título *</label>
            <asp:TextBox ID="txtTitulo" runat="server" CssClass="campo" MaxLength="120" />

            <label>Tipo de actividad *</label>
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="campo" />

            <label>Descripción *</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="campo" TextMode="MultiLine" Rows="3" />

            <div class="fila">
                <div><label>Fecha de inicio *</label><asp:TextBox ID="txtFechaInicio" runat="server" CssClass="campo" TextMode="Date" /></div>
                <div><label>Fecha de fin *</label><asp:TextBox ID="txtFechaFin" runat="server" CssClass="campo" TextMode="Date" /></div>
            </div>
            <div class="fila">
                <div><label>Hora de inicio *</label><asp:TextBox ID="txtHoraInicio" runat="server" CssClass="campo" TextMode="Time" /></div>
                <div><label>Hora de fin *</label><asp:TextBox ID="txtHoraFin" runat="server" CssClass="campo" TextMode="Time" /></div>
            </div>
            <div class="fila">
                <div>
                    <label>Modalidad *</label>
                    <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="campo">
                        <asp:ListItem>Presencial</asp:ListItem>
                        <asp:ListItem>Virtual</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div><label>Cupos *</label><asp:TextBox ID="txtCupos" runat="server" CssClass="campo" TextMode="Number" /></div>
            </div>
            <div class="fila">
                <div><label>Lugar *</label><asp:TextBox ID="txtLugar" runat="server" CssClass="campo" MaxLength="150" /></div>
                <div>
                    <label>Estado *</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="campo">
                        <asp:ListItem>Activa</asp:ListItem>
                        <asp:ListItem>Finalizada</asp:ListItem>
                        <asp:ListItem>Cancelada</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <asp:HiddenField ID="hfId" runat="server" Value="0" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-azul" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn" OnClick="btnLimpiar_Click" />
        </div>

        <div class="panel">
            <h2>Listado de Actividades Académicas</h2>
            <div class="fila">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="campo" placeholder="Buscar actividad..." />
                <asp:DropDownList ID="ddlFiltroTipo" runat="server" CssClass="campo" />
                <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="campo">
                    <asp:ListItem Value="">Todos los estados</asp:ListItem>
                    <asp:ListItem>Activa</asp:ListItem>
                    <asp:ListItem>Finalizada</asp:ListItem>
                    <asp:ListItem>Cancelada</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-azul" OnClick="btnBuscar_Click" />
            </div>

            <asp:GridView ID="gvActividades" runat="server" AutoGenerateColumns="False" CssClass="tabla" GridLines="None"
                EmptyDataText="No hay actividades." OnRowCommand="gvActividades_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ActividadId" HeaderText="#" />
                    <asp:BoundField DataField="Titulo" HeaderText="Título" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Cupos" HeaderText="Cupos" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <a class="btn-mini ver" href='Detalle.aspx?id=<%# Eval("ActividadId") %>'>Ver</a>
                            <asp:LinkButton runat="server" CssClass="btn-mini editar" CommandName="Editar" CommandArgument='<%# Eval("ActividadId") %>'>Editar</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn-mini eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("ActividadId") %>'
                                OnClientClick="return confirm('¿Eliminar esta actividad?');">Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
