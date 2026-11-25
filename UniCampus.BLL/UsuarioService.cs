using System.Security.Cryptography;
using System.Text;
using UniCampus.DAL;
using UniCampus.Entities;

namespace UniCampus.BLL
{
    public class UsuarioService
    {
        private UsuarioRepository repositorio = new UsuarioRepository();

        public Usuario Login(string correo, string clave)
        {
            if (correo.Trim() == "" || clave == "") return null;
            return repositorio.Buscar(correo.Trim(), Encriptar(clave));
        }

        private string Encriptar(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
