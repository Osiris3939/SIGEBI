using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Api.Controllers
{
    // Controlador de recursos bibliograficos
    [ApiController]
    [Route("api/[controller]")]
    public class RecursoBibliograficoController : ControllerBase
    {
        private readonly IRecursoBibliograficoService _recursoService;

        public RecursoBibliograficoController(IRecursoBibliograficoService recursoService)
        {
            _recursoService = recursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _recursoService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _recursoService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecursoBibliograficoDto dto)
        {
            var result = await _recursoService.AddAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecursoBibliograficoDto dto)
        {
            dto.Id = id;
            var result = await _recursoService.UpdateAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _recursoService.DeleteAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
