using System.Data;
using System.Data.SqlClient;
using UniCampus.Entities;

namespace UniCampus.DAL
{
    public class UsuarioRepository
    {
        public Usuario Buscar(string correo, string clave)
        {
            using (SqlConnection cn = Conexion.Crear())
            using (SqlCommand cmd = new SqlCommand("SELECT UsuarioId, Nombre, Correo FROM Usuario WHERE Correo = @Correo AND Clave = @Clave", cn))
            {
                cmd.Parameters.Add("@Correo", SqlDbType.NVarChar, 150).Value = correo;
                cmd.Parameters.Add("@Clave", SqlDbType.Char, 64).Value = clave;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new Usuario
                    {
                        UsuarioId = (int)dr["UsuarioId"],
                        Nombre = dr["Nombre"].ToString(),
                        Correo = dr["Correo"].ToString()
                    };
                }
                return null;
            }
        }
    }
}
