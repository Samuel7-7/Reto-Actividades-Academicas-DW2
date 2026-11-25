using System.Collections.Generic;
using UniCampus.Entities;

namespace UniCampus.Web.Views
{
    public interface IDetalleView
    {
        int ActividadId { get; }
        int UsuarioId { get; }
        int Calificacion { get; }
        string Comentario { get; }

        void MostrarActividad(Actividad actividad, bool estaInscrito);
        void MostrarInscritos(List<Inscripcion> inscritos);
        void MostrarResenas(List<Resena> resenas);
        void LimpiarResena();
        void MostrarMensaje(string mensaje, bool error);
    }
}
