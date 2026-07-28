using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    // Controlador de la capa de presentacion Web para Notificaciones
    public class NotificacionesController : Controller
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionesController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        // GET: Notificaciones
        public async Task<IActionResult> Index()
        {
            var notificaciones = await _notificacionService.GetAllAsync();
            return View(notificaciones);
        }

        // GET: Notificaciones/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var notificacion = await _notificacionService.GetByIdAsync(id);
            if (notificacion == null) return NotFound();
            return View(notificacion);
        }

        // GET: Notificaciones/Crear
        public IActionResult Crear()
        {
            return View(new NotificacionDto { FechaEnvio = DateTime.Now, Leida = false });
        }

        // POST: Notificaciones/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(NotificacionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var result = await _notificacionService.AddAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al registrar la notificacion.");
            return View(dto);
        }

        // GET: Notificaciones/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var notificacion = await _notificacionService.GetByIdAsync(id);
            if (notificacion == null) return NotFound();
            return View(notificacion);
        }

        // POST: Notificaciones/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, NotificacionDto dto)
        {
            dto.Id = id;
            if (!ModelState.IsValid) return View(dto);
            var result = await _notificacionService.UpdateAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al actualizar la notificacion.");
            return View(dto);
        }

        // POST: Notificaciones/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _notificacionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
