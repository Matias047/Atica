using GestiónUsuarios.Core.Entities;
using GestiónUsuarios.Core.Interfaces;
using GestiónUsuarios.Services;
using Moq;

namespace GestiónUsuarios.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CrearUsuarioAsync_DebeLlamarAlRepositorio_CuandoElUsuarioEsValido()
        {
            var nuevoUsuario = new Usuario { Nombre = "Matias", Email = "mati@test.com" };
            _repositoryMock.Setup(repo => repo.CrearAsync(nuevoUsuario))
                           .ReturnsAsync(1);

            await _service.CrearUsuarioAsync(nuevoUsuario);
            _repositoryMock.Verify(repo => repo.CrearAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task ObtenerUsuariosSegunRolAsync_DebeRetornarTodos_CuandoEsAdministrador()
        {
            var listaFicticia = new List<Usuario>
            {
                new Usuario { Id = 1, Rol = "Administrador" },
                new Usuario { Id = 2, Rol = "Usuario" }
            };
            _repositoryMock.Setup(repo => repo.ObtenerTodosAsync()).ReturnsAsync(listaFicticia);
            
            var resultado = await _service.ObtenerUsuariosSegunRolAsync("Administrador");

            Assert.Equal(2, resultado.Count());
            _repositoryMock.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerUsuariosSegunRolAsync_DebeFiltrarSoloUsuarios_CuandoNoEsAdministrador()
        {
            var listaFicticia = new List<Usuario>
            {
                new Usuario { Id = 1, Nombre = "Admin", Rol = "Administrador" },
                new Usuario { Id = 2, Nombre = "User1", Rol = "Usuario" },
                new Usuario { Id = 3, Nombre = "User2", Rol = "Usuario" }
            };
            _repositoryMock.Setup(repo => repo.ObtenerTodosAsync()).ReturnsAsync(listaFicticia);

            var resultado = await _service.ObtenerUsuariosSegunRolAsync("Usuario");

            Assert.Equal(2, resultado.Count());
            Assert.All(resultado, u => Assert.Equal("Usuario", u.Rol));
        }

        [Fact]
        public async Task CrearUsuarioAsync_DebeLlamarAlRepositorioUnaSolaVez()
        {
            var nuevoUsuario = new Usuario { Nombre = "Matias", Documento = "12345678" };
            _repositoryMock.Setup(repo => repo.CrearAsync(nuevoUsuario)).ReturnsAsync(1);

            var idGenerado = await _service.CrearUsuarioAsync(nuevoUsuario);

            Assert.Equal(1, idGenerado);
            _repositoryMock.Verify(repo => repo.CrearAsync(nuevoUsuario), Times.Once);
        }

        [Fact]
        public async Task EditarUsuarioAsync_DebeRetornarTrue_CuandoLaEdicionEsExitosa()
        {
            var usuarioAEditar = new Usuario
            {
                Id = 1,
                Nombre = "Matias Modificado",
                Rol = "Administrador"
            };

            _repositoryMock.Setup(repo => repo.EditarAsync(usuarioAEditar))
                           .ReturnsAsync(true);

            var resultado = await _service.EditarUsuarioAsync(usuarioAEditar);

            Assert.True(resultado);
            _repositoryMock.Verify(repo => repo.EditarAsync(usuarioAEditar), Times.Once);
        }

        [Fact]
        public async Task EditarUsuarioAsync_DebeRetornarFalse_CuandoElRepositorioFalla()
        {
            var usuarioInexistente = new Usuario { Id = 999, Nombre = "No Existo" };

            _repositoryMock.Setup(repo => repo.EditarAsync(usuarioInexistente))
                           .ReturnsAsync(false);

            var resultado = await _service.EditarUsuarioAsync(usuarioInexistente);

            Assert.False(resultado);
            _repositoryMock.Verify(repo => repo.EditarAsync(usuarioInexistente), Times.Once);
        }

        [Fact]
        public async Task EliminarUsuarioAsync_DebeRetornarTrue_CuandoElRepositorioEliminaExitosamente()
        {
            int idAEliminar = 99;
            _repositoryMock.Setup(repo => repo.EliminarAsync(idAEliminar)).ReturnsAsync(true);

            var resultado = await _service.EliminarUsuarioAsync(idAEliminar);

            Assert.True(resultado);
            _repositoryMock.Verify(repo => repo.EliminarAsync(idAEliminar), Times.Once);
        }
    }
}