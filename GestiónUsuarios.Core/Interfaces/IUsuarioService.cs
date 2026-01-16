using GestiónUsuarios.Core.Entities;

namespace GestiónUsuarios.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObtenerUsuariosSegunRolAsync(string rolActual);
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<int> CrearUsuarioAsync(Usuario usuario);
        Task<bool> EditarUsuarioAsync(Usuario usuario);
        Task<bool> EliminarUsuarioAsync(int id);
    }
}
