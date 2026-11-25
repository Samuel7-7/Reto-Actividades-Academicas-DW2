using System;
using UniCampus.BLL;
using UniCampus.Entities;
using UniCampus.Web.Views;

namespace UniCampus.Web.Presenters
{
    public class DetallePresenter
    {
        private IDetalleView vista;
        private ActividadService actividades = new ActividadService();
        private InscripcionService inscripciones = new InscripcionService();
        private ResenaService resenas = new ResenaService();

        public DetallePresenter(IDetalleView vista)
        {
            this.vista = vista;
        }

        public void Cargar()
        {
            try
            {
                Actividad actividad = actividades.Obtener(vista.ActividadId);
                if (actividad == null)
                {
                    vista.MostrarMensaje("La actividad no existe.", true);
                    return;
                }
                vista.MostrarActividad(actividad, inscripciones.EstaInscrito(vista.ActividadId, vista.UsuarioId));
                vista.MostrarInscritos(inscripciones.Listar(vista.ActividadId));
                vista.MostrarResenas(resenas.Listar(vista.ActividadId));
            }
            catch (Exception)
            {
                vista.MostrarMensaje("No se pudo cargar la actividad.", true);
            }
        }

        public void Inscribirme()
        {
            string error = inscripciones.Inscribir(vista.ActividadId, vista.UsuarioId);
            Cargar();
            if (error != "")
                vista.MostrarMensaje(error, true);
            else
                vista.MostrarMensaje("Inscripción realizada correctamente.", false);
        }

        public void PublicarResena()
        {
            string error = resenas.Publicar(vista.ActividadId, vista.UsuarioId, vista.Calificacion, vista.Comentario);
            Cargar();
            if (error != "")
            {
                vista.MostrarMensaje(error, true);
                return;
            }
            vista.LimpiarResena();
            vista.MostrarMensaje("Reseña publicada.", false);
        }
    }
}
