using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniCampus.Entities;

namespace UniCampus.DAL
{
    public class ResenaRepository
    {
        public List<Resena> Listar(int actividadId)
        {
            string sql = @"SELECT u.Nombre, r.Calificacion, r.Comentario, r.Fecha
                           FROM Resena r INNER JOIN Usuario u ON u.UsuarioId = r.UsuarioId
                           WHERE r.ActividadId = @ActividadId ORDER BY r.Fecha DESC";

            var lista = new List<Resena>();
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = actividadId;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Resena
                    {
                        Nombre = dr["Nombre"].ToString(),
                        Calificacion = (int)dr["Calificacion"],
                        Comentario = dr["Comentario"].ToString(),
                        Fecha = (DateTime)dr["Fecha"]
                    });
                }
            }
            return lista;
        }

        public bool Existe(int actividadId, int usuarioId)
        {
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Resena WHERE ActividadId = @ActividadId AND UsuarioId = @UsuarioId", cn))
            {
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = actividadId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public void Insertar(int actividadId, int usuarioId, int calificacion, string comentario)
        {
            string sql = @"INSERT INTO Resena (ActividadId, UsuarioId, Calificacion, Comentario)
                           VALUES (@ActividadId, @UsuarioId, @Calificacion, @Comentario)";

            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = actividadId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
                cmd.Parameters.Add("@Calificacion", SqlDbType.Int).Value = calificacion;
                cmd.Parameters.Add("@Comentario", SqlDbType.NVarChar, 500).Value = comentario;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
