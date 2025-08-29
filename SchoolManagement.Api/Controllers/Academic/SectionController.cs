using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Infrastructure.Services;

namespace SchoolManagement.Api.Controllers.Academic
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {
        private readonly SectionService _service;

        public SectionController(SectionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Permission("Academic Module", "Section", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("Academic Module", "Section", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Permission("Academic Module", "Section", "write")]
        public async Task<IActionResult> Create(SectionDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("Academic Module", "Section", "write")]
        public async Task<IActionResult> Update(int id, SectionDto dto)
        {
            if (id != dto.SectionId) return BadRequest("Mismatched SectionId");

            var result = await _service.UpdateAsync(dto);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("Academic Module", "Section", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
    }
}
