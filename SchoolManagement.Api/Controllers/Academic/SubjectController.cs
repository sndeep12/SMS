using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Infrastructure.Services;

namespace SchoolManagement.Api.Controllers.Academic
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService _service;

        public SubjectController(SubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        [Permission("Academic Module", "Subject", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("Academic Module", "Subject", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Permission("Academic Module", "Subject", "write")]
        public async Task<IActionResult> Create(SubjectDto dto)
        {
            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("Academic Module", "Subject", "write")]
        public async Task<IActionResult> Update(int id, SubjectDto dto)
        {
            if (id != dto.SubjectId) return BadRequest("Mismatched SubjectId");

            var result = await _service.UpdateAsync(dto);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("Academic Module", "Subject", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
    }

}
