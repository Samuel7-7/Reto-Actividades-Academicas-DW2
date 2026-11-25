using System;
using System.Collections.Generic;
using UniCampus.DAL;
using UniCampus.Entities;

namespace UniCampus.BLL
{
    public class InscripcionService
    {
        private InscripcionRepository repositorio = new InscripcionRepository();
        private ActividadRepository actividades = new ActividadRepository();

        public List<Inscripcion> Listar(int actividadId)
        {
            return repositorio.Listar(actividadId);
        }

        public bool EstaInscrito(int actividadId, int usuarioId)
        {
            return repositorio.EstaInscrito(actividadId, usuarioId);
        }

        public string Inscribir(int actividadId, int usuarioId)
        {
            Actividad a = actividades.ObtenerPorId(actividadId);
            if (a == null) return "La actividad no existe.";
            if (a.Estado != "Activa") return "La actividad no está activa.";
            if (a.FechaFin < DateTime.Today) return "La actividad ya terminó.";
            if (repositorio.EstaInscrito(actividadId, usuarioId)) return "Ya estás inscrito en esta actividad.";
            if (!repositorio.Inscribir(actividadId, usuarioId)) return "No hay cupos disponibles.";
            return "";
        }
    }
}
