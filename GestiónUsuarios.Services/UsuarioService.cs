using GestiónUsuarios.Core.Entities;
using GestiónUsuarios.Core.Interfaces;

namespace GestiónUsuarios.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosSegunRolAsync(string rolActual)
        {
            var todos = await _repository.ObtenerTodosAsync();

            if (rolActual == "Administrador")
            {
                return todos;
            }

            return todos.Where(u => u.Rol == "Usuario");
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id) => await _repository.ObtenerPorIdAsync(id);
        public async Task<int> CrearUsuarioAsync(Usuario usuario) => await _repository.CrearAsync(usuario);
        public async Task<bool> EditarUsuarioAsync(Usuario usuario) => await _repository.EditarAsync(usuario);
        public async Task<bool> EliminarUsuarioAsync(int id) => await _repository.EliminarAsync(id);
    }
}
