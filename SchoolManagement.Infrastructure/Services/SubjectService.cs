using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Infrastructure.Persistence;


namespace SchoolManagement.Infrastructure.Services
{
    public class SubjectService
    {
        private readonly IGenericRepository<Subject> _repository;

        public SubjectService(IGenericRepository<Subject> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var results = await _repository.Query()
                 .Include(s => s.School)
                 .Select(s => new SubjectDto
                 {
                     SubjectId = s.SubjectId,
                     SubjectName = s.SubjectName,
                     SubjectCode = s.SubjectCode,
                     SubjectType = s.SubjectType,
                     ShortName = s.ShortName,
                     StatusId = s.StatusId,
                     SchoolName = s.School.SchoolName
                 })
                .ToListAsync();
            return new ApiResponse(true, "Subjects retrieved successfully", results);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var result = await _repository.Query()
                .Include(s => s.School).Include(s => s.School).Select(
                s => new SubjectDto
                {
                    SubjectId = s.SubjectId,
                    SubjectName = s.SubjectName,
                    SubjectCode = s.SubjectCode,
                    SubjectType = s.SubjectType,
                    ShortName = s.ShortName,
                    StatusId = s.StatusId,
                    SchoolName = s.School.SchoolName
                })
                                           .FirstOrDefaultAsync(x => x.SubjectId == id);
            if (result == null)
                return new ApiResponse(false, "Subject not found");

            return new ApiResponse(true, "Subject retrieved successfully", result);
        }

        public async Task<ApiResponse> AddAsync(SubjectDto dto)
        {
            var data = new Subject
            {
                SchoolId = dto.SchoolId,
                SubjectName = dto.SubjectName,
                SubjectCode = dto.SubjectCode,
                SubjectType = dto.SubjectType,
                ShortName = dto.ShortName,
                StatusId = dto.StatusId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1 // TODO: replace with logged-in user
            };
           

            await _repository.AddAsync(data);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Section added successfully", data);
        }

        public async Task<ApiResponse> UpdateAsync(SubjectDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.SubjectId);
            if (existing == null)
                return new ApiResponse(false, "Subject not found");

            _repository.Detach(existing);

            var data = new Subject
            {
                SubjectId = dto.SubjectId,
                SchoolId = dto.SchoolId,
                ShortName = dto.ShortName,
                StatusId = dto.StatusId,
                SubjectCode = dto.SubjectCode,  
                SubjectName = dto.SubjectName,  
                SubjectType = dto.SubjectType,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = 1 // TODO: replace with logged-in user
            };

            _repository.Update(data);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Subject updated successfully", data);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "Subject not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Subject deleted successfully");
        }
    }

}
