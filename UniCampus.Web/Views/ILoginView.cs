using UniCampus.Entities;

namespace UniCampus.Web.Views
{
    public interface ILoginView
    {
        string Correo { get; }
        string Clave { get; }
        void Entrar(Usuario usuario);
        void MostrarMensaje(string mensaje);
    }
}
