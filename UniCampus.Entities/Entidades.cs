using System;

namespace UniCampus.Entities
{
    public class Actividad
    {
        public int ActividadId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Modalidad { get; set; }
        public string Lugar { get; set; }
        public int Cupos { get; set; }
        public string Estado { get; set; }
        public int Inscritos { get; set; }

        public int Disponibles
        {
            get { return Cupos - Inscritos; }
        }
    }

    public class TipoActividad
    {
        public int TipoId { get; set; }
        public string Nombre { get; set; }
    }

    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
    }

    public class Inscripcion
    {
        public string Nombre { get; set; }
        public string Programa { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }

    public class Resena
    {
        public string Nombre { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
