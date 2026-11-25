using System;
using UniCampus.BLL;
using UniCampus.Entities;
using UniCampus.Web.Views;

namespace UniCampus.Web.Presenters
{
    public class ActividadesPresenter
    {
        private IActividadesView vista;
        private ActividadService servicio = new ActividadService();

        public ActividadesPresenter(IActividadesView vista)
        {
            this.vista = vista;
        }

        public void Iniciar()
        {
            try
            {
                vista.MostrarTipos(servicio.ListarTipos());
                Buscar();
            }
            catch (Exception)
            {
                vista.MostrarMensaje("No se pudo conectar con la base de datos.", true);
            }
        }

        public void Buscar()
        {
            try
            {
                vista.MostrarActividades(servicio.Listar(vista.TextoBuscar, vista.TipoFiltro, vista.EstadoFiltro));
            }
            catch (Exception)
            {
                vista.MostrarMensaje("No se pudo cargar el listado.", true);
            }
        }

        public void Guardar()
        {
            try
            {
                string error = servicio.Guardar(vista.LeerFormulario());
                if (error != "")
                {
                    vista.MostrarMensaje(error, true);
                    return;
                }
                vista.MostrarMensaje("Actividad guardada correctamente.", false);
                vista.LimpiarFormulario();
                Buscar();
            }
            catch (Exception)
            {
                vista.MostrarMensaje("No se pudo guardar la actividad.", true);
            }
        }

        public void Editar(int id)
        {
            Actividad actividad = servicio.Obtener(id);
            if (actividad != null)
            {
                vista.CargarFormulario(actividad);
                vista.MostrarMensaje("Editando: " + actividad.Titulo, false);
            }
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
            vista.MostrarMensaje("La actividad fue cancelada.", false);
            Buscar();
        }
    }
}
