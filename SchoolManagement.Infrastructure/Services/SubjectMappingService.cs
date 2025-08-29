using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Academic;


namespace SchoolManagement.Infrastructure.Services
{
    public class SubjectMappingService
    {
        private readonly IGenericRepository<SubjectMapping> _repository;

        public SubjectMappingService(IGenericRepository<SubjectMapping> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var list = await _repository.Query()
                .Include(sm => sm.School)
                .Include(sm => sm.SectionMapping)
                    .ThenInclude(s => s.Section)
                .Include(sm => sm.Subject)
                .Include(sm => sm.User)
                .Select(sm => new SubjectMappingDto
                {
                    SubjectMappingId = sm.SubjectMappingId,
                    SchoolId = sm.SchoolId,
                    SchoolName = sm.School.SchoolName,
                    SectionMappingId = sm.SectionMappingId,
                    SectionName = sm.SectionMapping.Section.SectionName,
                    SubjectId = sm.SubjectId,
                    SubjectName = sm.Subject.SubjectName,
                    UserId = sm.UserId,
                    UserName = sm.User.Username, // adjust property
                    DisplayOrder = sm.DisplayOrder,
                    StatusId = sm.StatusId
                })
                .ToListAsync();

            return new ApiResponse(true, "Subject mappings retrieved successfully", list);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var sm = await _repository.Query()
                .Include(sm => sm.School)
                .Include(sm => sm.SectionMapping)
                    .ThenInclude(s => s.Section)
                .Include(sm => sm.Subject)
                .Include(sm => sm.User)
                .Where(sm => sm.SubjectMappingId == id)
                .Select(sm => new SubjectMappingDto
                {
                    SubjectMappingId = sm.SubjectMappingId,
                    SchoolId = sm.SchoolId,
                    SchoolName = sm.School.SchoolName,
                    SectionMappingId = sm.SectionMappingId,
                    SectionName = sm.SectionMapping.Section.SectionName,
                    SubjectId = sm.SubjectId,
                    SubjectName = sm.Subject.SubjectName,
                    UserId = sm.UserId,
                    UserName = sm.User.Username,
                    DisplayOrder = sm.DisplayOrder,
                    StatusId = sm.StatusId
                })
                .FirstOrDefaultAsync();

            if (sm == null)
                return new ApiResponse(false, "Subject mapping not found");

            return new ApiResponse(true, "Subject mapping retrieved successfully", sm);
        }

        public async Task<ApiResponse> AddAsync(SubjectMappingDto dto)
        {
            var sm = new SubjectMapping
            {
                SchoolId = dto.SchoolId,
                SectionMappingId = dto.SectionMappingId,
                SubjectId = dto.SubjectId,
                UserId = dto.UserId,
                DisplayOrder = dto.DisplayOrder,
                StatusId = dto.StatusId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1 // Replace with logged-in user
            };

            await _repository.AddAsync(sm);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Subject mapping added successfully", sm);
        }

        public async Task<ApiResponse> UpdateAsync(SubjectMappingDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.SubjectMappingId);
            if (existing == null)
                return new ApiResponse(false, "Subject mapping not found");

            _repository.Detach(existing);

            var sm = new SubjectMapping
            {
                SubjectMappingId = dto.SubjectMappingId,
                SchoolId = dto.SchoolId,
                SectionMappingId = dto.SectionMappingId,
                SubjectId = dto.SubjectId,
                UserId = dto.UserId,
                DisplayOrder = dto.DisplayOrder,
                StatusId = dto.StatusId,
                CreatedOn = existing.CreatedOn,
                CreatedBy = existing.CreatedBy,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = 1 // Replace with logged-in user
            };

            _repository.Update(sm);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Subject mapping updated successfully", sm);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "Subject mapping not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Subject mapping deleted successfully");
        }
    }
}
