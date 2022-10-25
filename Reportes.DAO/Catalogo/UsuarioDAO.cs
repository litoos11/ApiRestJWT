using Microsoft.Extensions.Options;
using Reportes.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Reportes.DAO
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly Conexion _conexion;

        public UsuarioDAO(IOptions<Conexion> conexion)
        {
            _conexion = conexion.Value;
        }
        public async Task<List<Usuario>> ObtieneTotos()
        {
            string storedPocedure = "dbo.api_R_Usuarios";
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(_conexion.LogaConnection))
                {
                    SqlCommand command = new SqlCommand(storedPocedure, conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            lista.Add(new Usuario()
                            {
                                Nombre = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "NOMBRE"),
                                Login = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "LOGIN"),
                                Contrasenia = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "CONTRASENIA"),
                                Email = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "EMAIL")
                            });
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario> ObtieneUsuarioPorCredenciales(UsuarioDTO usuario)
        {
            string storedProcedure = "dbo.api_R_UsuarioPorCredencial";
            Usuario usuarioRecuperado = new Usuario();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_conexion.LogaConnection))
                {
                    SqlCommand command = new SqlCommand(storedProcedure, conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Login", usuario.Usuario);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                    conexion.Open();

                    using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            usuarioRecuperado = new Usuario()
                            {
                                Nombre = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "NOMBRE"),
                                Login = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "LOGIN"),
                                Contrasenia = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "CONTRASENIA"),
                                Email = MetodosExtension.ObtenerValorColumnaPorTipo<string>(dataReader, "EMAIL")
                            };
                        }
                    }
                }
                return usuarioRecuperado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
