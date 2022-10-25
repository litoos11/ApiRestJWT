using Reportes.Entity;

namespace Reportes.DAO
{
    public interface IUsuarioDAO
    {
        Task<List<Usuario>> ObtieneTotos();
        Task<Usuario> ObtieneUsuarioPorCredenciales(UsuarioDTO usuario);
    }
}
