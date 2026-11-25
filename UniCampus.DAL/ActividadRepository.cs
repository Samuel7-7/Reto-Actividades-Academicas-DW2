using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniCampus.Entities;

namespace UniCampus.DAL
{
    public class ActividadRepository
    {
        private const string Consulta = @"
            SELECT a.*, t.Nombre AS Tipo,
                   (SELECT COUNT(*) FROM Inscripcion i WHERE i.ActividadId = a.ActividadId) AS Inscritos
            FROM Actividad a
            INNER JOIN TipoActividad t ON t.TipoId = a.TipoId ";

        public List<Actividad> Listar(string texto, int tipoId, string estado)
        {
            string sql = Consulta + @"
                WHERE a.Titulo LIKE @Texto
                  AND (@TipoId = 0 OR a.TipoId = @TipoId)
                  AND (@Estado = '' OR a.Estado = @Estado)
                ORDER BY a.FechaInicio DESC";

            var lista = new List<Actividad>();
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Texto", SqlDbType.NVarChar, 130).Value = "%" + texto + "%";
                cmd.Parameters.Add("@TipoId", SqlDbType.Int).Value = tipoId;
                cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 20).Value = estado;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(Leer(dr));
                }
            }
            return lista;
        }

        public Actividad ObtenerPorId(int id)
        {
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(Consulta + " WHERE a.ActividadId = @Id", cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return Leer(dr);
                }
                return null;
            }
        }

        public void Insertar(Actividad a)
        {
            string sql = @"INSERT INTO Actividad
                (Titulo, Descripcion, TipoId, FechaInicio, FechaFin, HoraInicio, HoraFin, Modalidad, Lugar, Cupos, Estado)
                VALUES (@Titulo, @Descripcion, @TipoId, @FechaInicio, @FechaFin, @HoraInicio, @HoraFin, @Modalidad, @Lugar, @Cupos, @Estado)";
            Ejecutar(sql, a);
        }

        public void Actualizar(Actividad a)
        {
            string sql = @"UPDATE Actividad SET
                Titulo = @Titulo, Descripcion = @Descripcion, TipoId = @TipoId,
                FechaInicio = @FechaInicio, FechaFin = @FechaFin, HoraInicio = @HoraInicio, HoraFin = @HoraFin,
                Modalidad = @Modalidad, Lugar = @Lugar, Cupos = @Cupos, Estado = @Estado
                WHERE ActividadId = @Id";
            Ejecutar(sql, a);
        }

        public void Eliminar(int id)
        {
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand("UPDATE Actividad SET Estado = 'Cancelada' WHERE ActividadId = @Id", cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<TipoActividad> ListarTipos()
        {
            var lista = new List<TipoActividad>();
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT TipoId, Nombre FROM TipoActividad ORDER BY Nombre", cn))
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new TipoActividad { TipoId = (int)dr["TipoId"], Nombre = dr["Nombre"].ToString() });
                }
            }
            return lista;
        }

        private void Ejecutar(string sql, Actividad a)
        {
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = a.ActividadId;
                cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 120).Value = a.Titulo;
                cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 1000).Value = a.Descripcion;
                cmd.Parameters.Add("@TipoId", SqlDbType.Int).Value = a.TipoId;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = a.FechaInicio;
                cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = a.FechaFin;
                cmd.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = a.HoraInicio;
                cmd.Parameters.Add("@HoraFin", SqlDbType.Time).Value = a.HoraFin;
                cmd.Parameters.Add("@Modalidad", SqlDbType.NVarChar, 20).Value = a.Modalidad;
                cmd.Parameters.Add("@Lugar", SqlDbType.NVarChar, 150).Value = a.Lugar;
                cmd.Parameters.Add("@Cupos", SqlDbType.Int).Value = a.Cupos;
                cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 20).Value = a.Estado;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Actividad Leer(SqlDataReader dr)
        {
            return new Actividad
            {
                ActividadId = (int)dr["ActividadId"],
                Titulo = dr["Titulo"].ToString(),
                Descripcion = dr["Descripcion"].ToString(),
                TipoId = (int)dr["TipoId"],
                Tipo = dr["Tipo"].ToString(),
                FechaInicio = (DateTime)dr["FechaInicio"],
                FechaFin = (DateTime)dr["FechaFin"],
                HoraInicio = (TimeSpan)dr["HoraInicio"],
                HoraFin = (TimeSpan)dr["HoraFin"],
                Modalidad = dr["Modalidad"].ToString(),
                Lugar = dr["Lugar"].ToString(),
                Cupos = (int)dr["Cupos"],
                Estado = dr["Estado"].ToString(),
                Inscritos = (int)dr["Inscritos"]
            };
        }
    }
}
