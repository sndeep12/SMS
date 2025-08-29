using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.DTOs.Academic;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class AcademicYearService
    {
        private readonly IGenericRepository<AcademicYear> _repository;

        public AcademicYearService(IGenericRepository<AcademicYear> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var academicYears = await _repository.Query()
        .Include(a => a.School) // load School navigation
        .Select(a => new AcademicYearDto
        {
            AcademicYearId = a.AcademicYearId,
            SchoolId = a.SchoolId,
            SchoolName = a.School.SchoolName,
            AcademicYearName = a.AcademicYearName,
            FromDate = a.FromDate,
            ToDate = a.ToDate,
            StatusId = a.StatusId
        })
        .ToListAsync();
            return new ApiResponse(true, "Academic Years retrieved successfully", academicYears);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var academicYear = await _repository.Query()
         .Include(a => a.School)
         .Where(a => a.AcademicYearId == id)
         .Select(a => new AcademicYearDto
         {
             AcademicYearId = a.AcademicYearId,
             SchoolId = a.SchoolId,
             SchoolName = a.School.SchoolName,
             AcademicYearName = a.AcademicYearName,
             FromDate = a.FromDate,
             ToDate = a.ToDate,
             StatusId = a.StatusId
         })
         .FirstOrDefaultAsync();
            if (academicYear == null)
                return new ApiResponse(false, "Academic Year not found");

            return new ApiResponse(true, "Academic Year retrieved successfully", academicYear);
        }

        public async Task<ApiResponse> AddAsync(AcademicYearDto dto)
        {
            var academicYear = new AcademicYear
            {
                SchoolId = dto.SchoolId,
                AcademicYearName = dto.AcademicYearName,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                StatusId = dto.StatusId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1 // Replace with actual logged-in user ID
            };

            await _repository.AddAsync(academicYear);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Academic Year added successfully", academicYear);
        }

        public async Task<ApiResponse> UpdateAsync(AcademicYearDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.AcademicYearId);
            if (existing == null)
                return new ApiResponse(false, "Academic Year not found");

            // Detach tracked entity to avoid EF conflict
            _repository.Detach(existing);

            var academicYear = new AcademicYear
            {
                AcademicYearId = dto.AcademicYearId,
                SchoolId = dto.SchoolId,
                AcademicYearName = dto.AcademicYearName,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                StatusId = dto.StatusId,
                CreatedOn = existing.CreatedOn,
                CreatedBy = existing.CreatedBy,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = 1 // Replace with actual logged-in user ID
            };

            _repository.Update(academicYear);
            await _repository.SaveChangesAsync();

            return new ApiResponse(true, "Academic Year updated successfully", academicYear);
        }
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "Academic Year not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();
            return new ApiResponse(true, "Academic Year deleted successfully");
        }
    }

}
