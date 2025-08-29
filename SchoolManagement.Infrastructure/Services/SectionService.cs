using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Academic;

namespace SchoolManagement.Infrastructure.Services
{
    public class SectionService
    {
        private readonly IGenericRepository<Section> _repository;

        public SectionService(IGenericRepository<Section> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var sections = await _repository.Query()
                .Include(s => s.School)
                .Select(s => new SectionDto
                {
                    SectionId = s.SectionId,
                    SchoolId = s.SchoolId,
                    SchoolName = s.School.SchoolName,
                    SectionName = s.SectionName,
                    StatusId = s.StatusId,
                    DisplayOrder = s.DisplayOrder
                })
                .ToListAsync();

            return new ApiResponse(true, "Sections retrieved successfully", sections);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var section = await _repository.Query()
                .Include(s => s.School)
                .Where(s => s.SectionId == id)
                .Select(s => new SectionDto
                {
                    SectionId = s.SectionId,
                    SchoolId = s.SchoolId,
                    SchoolName = s.School.SchoolName,
                    SectionName = s.SectionName,
                    StatusId = s.StatusId,
                    DisplayOrder = s.DisplayOrder
                })
                .FirstOrDefaultAsync();

            if (section == null)
                return new ApiResponse(false, "Section not found");

            return new ApiResponse(true, "Section retrieved successfully", section);
        }

        public async Task<ApiResponse> AddAsync(SectionDto dto)
        {
            var section = new Section
            {
                SchoolId = dto.SchoolId,
                SectionName = dto.SectionName,
                StatusId = dto.StatusId,
                DisplayOrder = dto.DisplayOrder,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1 // TODO: replace with logged-in user
            };

            await _repository.AddAsync(section);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Section added successfully", section);
        }

        public async Task<ApiResponse> UpdateAsync(SectionDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.SectionId);
            if (existing == null)
                return new ApiResponse(false, "Section not found");

            _repository.Detach(existing);

            var section = new Section
            {
                SectionId = dto.SectionId,
                SchoolId = dto.SchoolId,
                SectionName = dto.SectionName,
                StatusId = dto.StatusId,
                DisplayOrder = dto.DisplayOrder,
                CreatedOn = existing.CreatedOn,
                CreatedBy = existing.CreatedBy,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = 1 // TODO: replace with logged-in user
            };

            _repository.Update(section);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Section updated successfully", section);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "Section not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Section deleted successfully");
        }
    }
}
