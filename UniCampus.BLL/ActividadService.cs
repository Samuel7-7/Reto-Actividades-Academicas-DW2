using System;
using System.Collections.Generic;
using UniCampus.DAL;
using UniCampus.Entities;

namespace UniCampus.BLL
{
    public class ActividadService
    {
        private ActividadRepository repositorio = new ActividadRepository();

        public List<Actividad> Listar(string texto, int tipoId, string estado)
        {
            return repositorio.Listar(texto.Trim(), tipoId, estado);
        }

        public Actividad Obtener(int id)
        {
            return repositorio.ObtenerPorId(id);
        }

        public List<TipoActividad> ListarTipos()
        {
            return repositorio.ListarTipos();
        }

        public string Guardar(Actividad a)
        {
            if (a.Titulo.Trim() == "") return "El título es obligatorio.";
            if (a.Descripcion.Trim() == "") return "La descripción es obligatoria.";
            if (a.TipoId == 0) return "Seleccione el tipo de actividad.";
            if (a.FechaInicio == DateTime.MinValue || a.FechaFin == DateTime.MinValue) return "Las fechas son obligatorias.";
            if (a.FechaFin < a.FechaInicio) return "La fecha de fin no puede ser menor que la fecha de inicio.";
            if (a.HoraFin <= a.HoraInicio) return "La hora de fin debe ser mayor que la hora de inicio.";
            if (a.Lugar.Trim() == "") return "El lugar es obligatorio.";
            if (a.Cupos <= 0) return "Los cupos deben ser mayores que cero.";

            if (a.ActividadId == 0)
            {
                if (a.FechaInicio < DateTime.Today) return "La fecha de inicio no puede ser anterior a hoy.";
                repositorio.Insertar(a);
            }
            else
            {
                Actividad actual = repositorio.ObtenerPorId(a.ActividadId);
                if (a.Cupos < actual.Inscritos) return "Los cupos no pueden ser menores que los inscritos (" + actual.Inscritos + ").";
                repositorio.Actualizar(a);
            }
            return "";
        }

        public void Eliminar(int id)
        {
            repositorio.Eliminar(id);
        }
    }
}
