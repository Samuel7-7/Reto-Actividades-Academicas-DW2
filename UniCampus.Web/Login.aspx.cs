using System;
using UniCampus.Entities;
using UniCampus.Web.Presenters;
using UniCampus.Web.Views;

namespace UniCampus.Web
{
    public partial class Login : System.Web.UI.Page, ILoginView
    {
        private LoginPresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new LoginPresenter(this);
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            presenter.Ingresar();
        }

        public string Correo
        {
            get { return txtCorreo.Text; }
        }

        public string Clave
        {
            get { return txtClave.Text; }
        }

        public void Entrar(Usuario usuario)
        {
            Session["UsuarioId"] = usuario.UsuarioId;
            Session["Nombre"] = usuario.Nombre;
            Response.Redirect("Actividades.aspx", false);
        }

        public void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }
    }
}
