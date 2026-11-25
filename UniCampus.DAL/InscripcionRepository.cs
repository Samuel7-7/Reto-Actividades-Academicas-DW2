using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniCampus.Entities;

namespace UniCampus.DAL
{
    public class InscripcionRepository
    {
        public List<Inscripcion> Listar(int actividadId)
        {
            string sql = @"SELECT u.Nombre, u.Programa, i.FechaInscripcion
                           FROM Inscripcion i INNER JOIN Usuario u ON u.UsuarioId = i.UsuarioId
                           WHERE i.ActividadId = @ActividadId ORDER BY i.FechaInscripcion";

            var lista = new List<Inscripcion>();
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = actividadId;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Inscripcion
                    {
                        Nombre = dr["Nombre"].ToString(),
                        Programa = dr["Programa"].ToString(),
                        FechaInscripcion = (DateTime)dr["FechaInscripcion"]
                    });
                }
            }
            return lista;
        }

        public bool EstaInscrito(int actividadId, int usuarioId)
        {
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Inscripcion WHERE ActividadId = @ActividadId AND UsuarioId = @UsuarioId", cn))
            {
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = actividadId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool Inscribir(int actividadId, int usuarioId)
        {
            string sql = @"INSERT INTO Inscripcion (ActividadId, UsuarioId)
                           SELECT @ActividadId, @UsuarioId
                           WHERE (SELECT COUNT(*) FROM Inscripcion WITH (UPDLOCK, HOLDLOCK) WHERE ActividadId = @ActividadId)
                               < (SELECT Cupos FROM Actividad WHERE ActividadId = @ActividadId)";

            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = actividadId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
