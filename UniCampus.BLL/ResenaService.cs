using System.Collections.Generic;
using UniCampus.DAL;
using UniCampus.Entities;

namespace UniCampus.BLL
{
    public class ResenaService
    {
        private ResenaRepository repositorio = new ResenaRepository();
        private InscripcionRepository inscripciones = new InscripcionRepository();

        public List<Resena> Listar(int actividadId)
        {
            return repositorio.Listar(actividadId);
        }

        public string Publicar(int actividadId, int usuarioId, int calificacion, string comentario)
        {
            if (calificacion < 1 || calificacion > 5) return "La calificación debe estar entre 1 y 5.";
            if (comentario.Length > 500) return "El comentario no puede tener más de 500 caracteres.";
            if (!inscripciones.EstaInscrito(actividadId, usuarioId)) return "Solo los inscritos pueden dejar una reseña.";
            if (repositorio.Existe(actividadId, usuarioId)) return "Ya dejaste una reseña en esta actividad.";

            repositorio.Insertar(actividadId, usuarioId, calificacion, comentario.Trim());
            return "";
        }
    }
}
