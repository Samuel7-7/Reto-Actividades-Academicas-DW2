using System.Configuration;
using System.Data.SqlClient;

namespace UniCampus.DAL
{
    public static class Conexion
    {
        public static SqlConnection Crear()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["UniCampusDb"].ConnectionString);
        }
    }
}
