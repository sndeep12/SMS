using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Infrastructure.Services;
using System.Runtime.InteropServices;

namespace SchoolManagement.Api.Controllers.Academic
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediumController : ControllerBase
    {
        private readonly MediumService _service;

        public MediumController(MediumService service)
        {
            _service = service;
        }

        [HttpGet]
        [Permission("Academic Module", "Medium", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("Academic Module", "Medium", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Permission("Academic Module", "Medium", "write")]
        public async Task<IActionResult> Create(MediumDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("Academic Module", "Medium", "write")]
        public async Task<IActionResult> Update(int id, MediumDto dto)
        {
            if (id != dto.MediumId) return BadRequest("Mismatched MediumId");

            var result = await _service.UpdateAsync(dto);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("Academic Module", "Medium", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
    }
}
