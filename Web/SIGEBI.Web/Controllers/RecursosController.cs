using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    // Controlador de la capa de presentacion Web para Catalogo de Recursos
    public class RecursosController : Controller
    {
        private readonly IRecursoBibliograficoService _recursoService;

        public RecursosController(IRecursoBibliograficoService recursoService)
        {
            _recursoService = recursoService;
        }

        // GET: Recursos
        public async Task<IActionResult> Index()
        {
            var recursos = await _recursoService.GetAllAsync();
            return View(recursos);
        }

        // GET: Recursos/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var recurso = await _recursoService.GetByIdAsync(id);
            if (recurso == null) return NotFound();
            return View(recurso);
        }

        // GET: Recursos/Crear
        public IActionResult Crear()
        {
            return View(new RecursoBibliograficoDto { AnioPublicacion = 2024, CategoriaId = 1 });
        }

        // POST: Recursos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(RecursoBibliograficoDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var result = await _recursoService.AddAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al registrar el recurso.");
            return View(dto);
        }

        // GET: Recursos/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var recurso = await _recursoService.GetByIdAsync(id);
            if (recurso == null) return NotFound();
            return View(recurso);
        }

        // POST: Recursos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, RecursoBibliograficoDto dto)
        {
            dto.Id = id;
            if (!ModelState.IsValid) return View(dto);
            var result = await _recursoService.UpdateAsync(dto);
            if (result.Success) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", result.Message ?? "Error al actualizar el recurso.");
            return View(dto);
        }

        // POST: Recursos/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _recursoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
