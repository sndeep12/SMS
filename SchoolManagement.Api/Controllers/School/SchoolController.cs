using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Infrastructure.Services;

namespace SchoolManagement.Api.Controllers.School
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolService _schoolService;

        public SchoolController(SchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [Permission("School Configuration", "School", "read")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _schoolService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Permission("School Configuration", "School", "read")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _schoolService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Permission("School Configuration", "School", "write")]
        public async Task<IActionResult> Create(Domain.Entities.School school)
        {
            var result = await _schoolService.AddAsync(school);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Permission("School Configuration", "School", "write")]
        public async Task<IActionResult> Update(int id, Domain.Entities.School school)
        {
            if (id != school.SchoolId) return BadRequest();

            var result = await _schoolService.UpdateAsync(school);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission("School Configuration", "School", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _schoolService.DeleteAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
