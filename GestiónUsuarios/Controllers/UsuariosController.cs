using GestiónUsuarios.Core.Entities;
using GestiónUsuarios.Core.Interfaces;
using GestiónUsuarios.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestiónUsuarios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly string _rolSimulado = "Administrador"; // Administrador - Usuario

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosSegunRolAsync(_rolSimulado);

            var modelo = usuarios.Select(u => new UsuarioViewModels
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Documento = u.Documento,
                Email = u.Email,
                Rol = u.Rol
            });

            ViewBag.RolActual = _rolSimulado;
            return View(modelo);
        }

        public IActionResult Create()
        {
            ViewBag.RolActual = _rolSimulado;

            return View("Crear");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(UsuarioViewModels model)
        {

            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Documento = model.Documento,
                    Email = model.Email,
                    Rol = model.Rol
                };

                await _usuarioService.CrearUsuarioAsync(usuario);
                TempData["Mensaje"] = "Usuario guardado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RolActual = _rolSimulado;

            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuarioService.ObtenerPorIdAsync(id);
            if (usuario == null) return NotFound();

            var model = new UsuarioViewModels
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Documento = usuario.Documento,
                Email = usuario.Email,
                Rol = usuario.Rol
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, UsuarioViewModels model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Documento = model.Documento,
                    Email = model.Email,
                    Rol = model.Rol
                };

                await _usuarioService.EditarUsuarioAsync(usuario);
                TempData["Mensaje"] = "Usuario editado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _usuarioService.EliminarUsuarioAsync(id);
            TempData["Mensaje"] = "Usuario eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
