using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    // Controlador de la capa de presentacion Web para Penalizaciones
    public class PenalizacionesController : Controller
    {
        private readonly IPenalizacionService _penalizacionService;

        public PenalizacionesController(IPenalizacionService penalizacionService)
        {
            _penalizacionService = penalizacionService;
        }

        // GET: Penalizaciones
        public async Task<IActionResult> Index()
        {
            var penalizaciones = await _penalizacionService.GetAllAsync();
            return View(penalizaciones);
        }

        // GET: Penalizaciones/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var penalizacion = await _penalizacionService.GetByIdAsync(id);
            if (penalizacion == null) return NotFound();
            return View(penalizacion);
        }

        // GET: Penalizaciones/Crear
        public IActionResult Crear()
        {
            return View(new PenalizacionDto { MontoMulta = 50.00m, Pagada = false });
        }

        // POST: Penalizaciones/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PenalizacionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var result = await _penalizacionService.AddAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al registrar la penalizacion.");
            return View(dto);
        }

        // GET: Penalizaciones/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var penalizacion = await _penalizacionService.GetByIdAsync(id);
            if (penalizacion == null) return NotFound();
            return View(penalizacion);
        }

        // POST: Penalizaciones/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, PenalizacionDto dto)
        {
            dto.Id = id;
            if (!ModelState.IsValid) return View(dto);
            var result = await _penalizacionService.UpdateAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al actualizar la penalizacion.");
            return View(dto);
        }

        // POST: Penalizaciones/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _penalizacionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
