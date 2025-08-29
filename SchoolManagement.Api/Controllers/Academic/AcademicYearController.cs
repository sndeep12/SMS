using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Infrastructure.Services;

namespace SchoolManagement.Api.Controllers.Academic
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademicYearController : ControllerBase
    {
        private readonly AcademicYearService _service;

        public AcademicYearController(AcademicYearService service)
        {
            _service = service;
        }

        [HttpGet]
        [Permission("Academic Module", "AcademicYear", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("Academic Module", "AcademicYear", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Permission("Academic Module", "AcademicYear", "write")]
        public async Task<IActionResult> Create(AcademicYearDto academicYear)
        {
            var result = await _service.AddAsync(academicYear);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("Academic Module", "AcademicYear", "write")]
        public async Task<IActionResult> Update(int id, AcademicYearDto academicYear)
        {
            if (id != academicYear.AcademicYearId) return BadRequest();

            var result = await _service.UpdateAsync(academicYear);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("Academic Module", "AcademicYear", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }

}
