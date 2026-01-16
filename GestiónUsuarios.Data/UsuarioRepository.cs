using Dapper;
using GestiónUsuarios.Core.Entities;
using GestiónUsuarios.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace GestiónUsuarios.Data
{
    public class UsuarioRepository :IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            using var db = new SqlConnection(_connectionString);
            return await db.QueryAsync<Usuario>("SELECT * FROM Usuarios");
        }
        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM Usuarios WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }

        public async Task<int> CrearAsync(Usuario usuario)
        {
            using var db = new SqlConnection(_connectionString);
            string sql = @"INSERT INTO Usuarios (Nombre, Apellido, Documento, Email, Rol) 
                           VALUES (@Nombre, @Apellido, @Documento, @Email, @Rol)";
            return await db.ExecuteAsync(sql, usuario);
        }

        public async Task<bool> EditarAsync(Usuario usuario)
        {
            using var db = new SqlConnection(_connectionString);
            string sql = @"UPDATE Usuarios 
                           SET Nombre = @Nombre, Apellido = @Apellido, Documento = @Documento, Email = @Email, Rol = @Rol 
                           WHERE Id = @Id";
            var filasAfectadas = await db.ExecuteAsync(sql, usuario);
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            string sql = "DELETE FROM Usuarios WHERE Id = @Id";
            return await db.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }
    }
}
