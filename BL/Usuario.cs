using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static (bool, string?, Exception) AddUsuario(ML.Usuario usuario)
        {
			try
			{
				using (DL.JoaxacaTrackTraceContext context = new DL.JoaxacaTrackTraceContext())
				{
					DL.Usuario objUsuario = new DL.Usuario();

					objUsuario.Nombre = usuario.NombreUsuario;
					objUsuario.ApellidoPaterno = usuario.ApellidoPaterno;
					objUsuario.ApelldioMaterno = usuario.ApellidoMaterno;
					objUsuario.Email = usuario.Email;
					objUsuario.Password = usuario.Password;

					context.Usuarios.Add(objUsuario);

					int rowsAffected = context.SaveChanges();

					if (rowsAffected > 0)
					{
						return (true,"Se agrego Exitosamente", null);
					}
					else
					{
						return (false, "No se pudo agregar", null);
					}
				}
			}
			catch (Exception ex)
			{

				return (false, ex.Message, ex);
			}
        }
    }
}
