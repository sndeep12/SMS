using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Academic;

namespace SchoolManagement.Api.Controllers.Academic
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionMappingController : ControllerBase
    {
        private readonly SectionMappingService _service;

        public SectionMappingController(SectionMappingService service)
        {
            _service = service;
        }

        [HttpGet]
        [Permission("Academic Module", "SectionMapping", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("Academic Module", "SectionMapping", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Permission("Academic Module", "SectionMapping", "write")]
        public async Task<IActionResult> Create([FromBody] SectionMappingDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("Academic Module", "SectionMapping", "write")]
        public async Task<IActionResult> Update(int id, [FromBody] SectionMappingDto dto)
        {
            if (id != dto.SectionMappingId) return BadRequest("Mismatched ID");

            var result = await _service.UpdateAsync(dto);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("Academic Module", "SectionMapping", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
    }

}
