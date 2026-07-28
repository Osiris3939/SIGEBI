using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    // Controlador de la capa de presentacion Web para Prestamos
    public class PrestamosController : Controller
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var prestamos = await _prestamoService.GetAllAsync();
            return View(prestamos);
        }

        // GET: Prestamos/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var prestamo = await _prestamoService.GetByIdAsync(id);
            if (prestamo == null) return NotFound();
            return View(prestamo);
        }

        // GET: Prestamos/Crear
        public IActionResult Crear()
        {
            return View(new PrestamoDto
            {
                FechaPrestamo = DateTime.Now,
                FechaDevolucionPactada = DateTime.Now.AddDays(7),
                EstadoPrestamo = "Activo"
            });
        }

        // POST: Prestamos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PrestamoDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var result = await _prestamoService.AddAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al registrar el prestamo.");
            return View(dto);
        }

        // GET: Prestamos/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var prestamo = await _prestamoService.GetByIdAsync(id);
            if (prestamo == null) return NotFound();
            return View(prestamo);
        }

        // POST: Prestamos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, PrestamoDto dto)
        {
            dto.Id = id;
            if (!ModelState.IsValid) return View(dto);
            var result = await _prestamoService.UpdateAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al actualizar el prestamo.");
            return View(dto);
        }

        // POST: Prestamos/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _prestamoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
