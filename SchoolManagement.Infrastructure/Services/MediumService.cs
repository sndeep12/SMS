using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Academic;

namespace SchoolManagement.Infrastructure.Services
{
    public class MediumService
    {
        private readonly IGenericRepository<Medium> _repository;

        public MediumService(IGenericRepository<Medium> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var mediums = await _repository.Query()
                .Include(m => m.School)
                .Select(m => new MediumDto
                {
                    MediumId = m.MediumId,
                    SchoolId = m.SchoolId,
                    SchoolName = m.School.SchoolName,
                    MediumName = m.MediumName,
                    ShortName = m.ShortName,
                    StatusId = m.StatusId
                })
                .ToListAsync();

            return new ApiResponse(true, "Mediums retrieved successfully", mediums);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var medium = await _repository.Query()
                .Include(m => m.School)
                .Where(m => m.MediumId == id)
                .Select(m => new MediumDto
                {
                    MediumId = m.MediumId,
                    SchoolId = m.SchoolId,
                    SchoolName = m.School.SchoolName,
                    MediumName = m.MediumName,
                    ShortName = m.ShortName,
                    StatusId = m.StatusId
                })
                .FirstOrDefaultAsync();

            if (medium == null)
                return new ApiResponse(false, "Medium not found");

            return new ApiResponse(true, "Medium retrieved successfully", medium);
        }

        public async Task<ApiResponse> AddAsync(MediumDto dto)
        {
            var medium = new Medium
            {
                SchoolId = dto.SchoolId,
                MediumName = dto.MediumName,
                ShortName = dto.ShortName,
                StatusId = dto.StatusId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1 // TODO: replace with logged-in user ID
            };

            await _repository.AddAsync(medium);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Medium added successfully", medium);
        }

        public async Task<ApiResponse> UpdateAsync(MediumDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.MediumId);
            if (existing == null)
                return new ApiResponse(false, "Medium not found");

            _repository.Detach(existing); // avoid EF tracking conflict

            var medium = new Medium
            {
                MediumId = dto.MediumId,
                SchoolId = dto.SchoolId,
                MediumName = dto.MediumName,
                ShortName = dto.ShortName,
                StatusId = dto.StatusId,
                CreatedOn = existing.CreatedOn,
                CreatedBy = existing.CreatedBy,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = 1 // TODO: replace with logged-in user ID
            };

            _repository.Update(medium);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Medium updated successfully", medium);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "Medium not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Medium deleted successfully");
        }
    }
}
