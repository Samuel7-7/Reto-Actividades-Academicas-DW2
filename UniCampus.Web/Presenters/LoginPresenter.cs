using System;
using UniCampus.BLL;
using UniCampus.Entities;
using UniCampus.Web.Views;

namespace UniCampus.Web.Presenters
{
    public class LoginPresenter
    {
        private ILoginView vista;
        private UsuarioService servicio = new UsuarioService();

        public LoginPresenter(ILoginView vista)
        {
            this.vista = vista;
        }

        public void Ingresar()
        {
            try
            {
                Usuario usuario = servicio.Login(vista.Correo, vista.Clave);
                if (usuario == null)
                    vista.MostrarMensaje("Correo o contraseña incorrectos.");
                else
                    vista.Entrar(usuario);
            }
            catch (Exception)
            {
                vista.MostrarMensaje("No se pudo conectar con la base de datos.");
            }
        }
    }
}
