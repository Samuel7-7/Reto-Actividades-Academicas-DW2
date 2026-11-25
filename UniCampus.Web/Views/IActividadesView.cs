using System.Collections.Generic;
using UniCampus.Entities;

namespace UniCampus.Web.Views
{
    public interface IActividadesView
    {
        string TextoBuscar { get; }
        int TipoFiltro { get; }
        string EstadoFiltro { get; }

        Actividad LeerFormulario();
        void CargarFormulario(Actividad actividad);
        void LimpiarFormulario();
        void MostrarTipos(List<TipoActividad> tipos);
        void MostrarActividades(List<Actividad> actividades);
        void MostrarMensaje(string mensaje, bool error);
    }
}
