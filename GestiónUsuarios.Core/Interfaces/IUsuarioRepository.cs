using GestiónUsuarios.Core.Entities;

namespace GestiónUsuarios.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Usuario usuario);
        Task<bool> EditarAsync(Usuario usuario);
        Task<bool> EliminarAsync(int id);
    }
}
