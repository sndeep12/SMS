using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Infrastructure.Services;

namespace SchoolManagement.Api.Controllers.Academic
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly ClassService _service;

        public ClassController(ClassService service)
        {
            _service = service;
        }

        [HttpGet]
        [Permission("Academic Module", "Class", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("Academic Module", "Class", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Permission("Academic Module", "Class", "write")]
        public async Task<IActionResult> Create(ClassDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("Academic Module", "Class", "write")]
        public async Task<IActionResult> Update(int id, ClassDto dto)
        {
            if (id != dto.ClassId) return BadRequest("Mismatched ClassId");

            var result = await _service.UpdateAsync(dto);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("Academic Module", "Class", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
    }
}
