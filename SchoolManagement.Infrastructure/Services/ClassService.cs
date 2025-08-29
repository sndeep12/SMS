using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Academic;

namespace SchoolManagement.Infrastructure.Services
{
    public class ClassService
    {
        private readonly IGenericRepository<Class> _repository;

        public ClassService(IGenericRepository<Class> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var classes = await _repository.Query()
                .Include(c => c.School)
                .Select(c => new ClassDto
                {
                    ClassId = c.ClassId,
                    SchoolId = c.SchoolId,
                    SchoolName = c.School.SchoolName,
                    ClassName = c.ClassName,
                    StatusId = c.StatusId,
                    DisplayOrder = c.DisplayOrder
                })
                .ToListAsync();

            return new ApiResponse(true, "Classes retrieved successfully", classes);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var cls = await _repository.Query()
                .Include(c => c.School)
                .Where(c => c.ClassId == id)
                .Select(c => new ClassDto
                {
                    ClassId = c.ClassId,
                    SchoolId = c.SchoolId,
                    SchoolName = c.School.SchoolName,
                    ClassName = c.ClassName,
                    StatusId = c.StatusId,
                    DisplayOrder = c.DisplayOrder
                })
                .FirstOrDefaultAsync();

            if (cls == null)
                return new ApiResponse(false, "Class not found");

            return new ApiResponse(true, "Class retrieved successfully", cls);
        }

        public async Task<ApiResponse> AddAsync(ClassDto dto)
        {
            var cls = new Class
            {
                SchoolId = dto.SchoolId,
                ClassName = dto.ClassName,
                StatusId = dto.StatusId,
                DisplayOrder = dto.DisplayOrder,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1 // TODO: replace with logged-in user
            };

            await _repository.AddAsync(cls);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Class added successfully", cls);
        }

        public async Task<ApiResponse> UpdateAsync(ClassDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.ClassId);
            if (existing == null)
                return new ApiResponse(false, "Class not found");

            // Detach existing tracked entity
            _repository.Detach(existing);

            var cls = new Class
            {
                ClassId = dto.ClassId,
                SchoolId = dto.SchoolId,
                ClassName = dto.ClassName,
                StatusId = dto.StatusId,
                DisplayOrder = dto.DisplayOrder,
                CreatedOn = existing.CreatedOn,
                CreatedBy = existing.CreatedBy,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = 1 // TODO: replace with logged-in user
            };

            _repository.Update(cls);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Class updated successfully", cls);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "Class not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Class deleted successfully");
        }
    }
}
