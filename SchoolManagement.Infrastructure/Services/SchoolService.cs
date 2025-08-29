using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class SchoolService
    {
        private readonly IGenericRepository<School> _repository;

        public SchoolService(IGenericRepository<School> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var schools = await _repository.GetAllAsync();
            return new ApiResponse(true, "Schools retrieved successfully", schools);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var school = await _repository.GetByIdAsync(id);
            if (school == null)
                return new ApiResponse(false, "School not found");

            return new ApiResponse(true, "School retrieved successfully", school);
        }

        public async Task<ApiResponse> AddAsync(School school)
        {
            school.CreatedOn = DateTime.UtcNow;
            await _repository.AddAsync(school);
            await _repository.SaveChangesAsync();
            return new ApiResponse(true, "School added successfully", school);
        }

        public async Task<ApiResponse> UpdateAsync(School school)
        {
            var existing = await _repository.GetByIdAsync(school.SchoolId);
            if (existing == null)
                return new ApiResponse(false, "School not found");
            // Detach the tracked entity
            _repository.Detach(existing);

            _repository.Update(school);
            await _repository.SaveChangesAsync();
            return new ApiResponse(true, "School updated successfully", school);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse(false, "School not found");

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();
            return new ApiResponse(true, "School deleted successfully");
        }
    }
}
