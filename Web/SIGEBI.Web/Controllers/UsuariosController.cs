using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    // Controlador de la capa de presentacion Web para Usuarios
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // GET: Usuarios/Crear
        public IActionResult Crear()
        {
            return View(new UsuarioDto());
        }

        // POST: Usuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(UsuarioDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var result = await _usuarioService.AddAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al registrar el usuario.");
            return View(dto);
        }

        // GET: Usuarios/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // POST: Usuarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, UsuarioDto dto)
        {
            dto.Id = id;
            if (!ModelState.IsValid) return View(dto);
            var result = await _usuarioService.UpdateAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al actualizar el usuario.");
            return View(dto);
        }

        // POST: Usuarios/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _usuarioService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
