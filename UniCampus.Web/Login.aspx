<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniCampus.Web.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>UniCampus - Iniciar sesión</title>
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body class="login-fondo">
    <form id="form1" runat="server">
        <div class="login-caja">
            <h1>UniCampus</h1>
            <p>Registro de Actividades Académicas</p>
            <asp:Label ID="lblMensaje" runat="server" CssClass="error" />
            <label>Correo</label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="campo" />
            <label>Contraseña</label>
            <asp:TextBox ID="txtClave" runat="server" CssClass="campo" TextMode="Password" />
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-azul" OnClick="btnIngresar_Click" />
            <p class="ayuda">Usuario de prueba: samuel@unicampus.edu / 123456</p>
        </div>
    </form>
</body>
</html>
