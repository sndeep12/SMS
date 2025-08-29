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

public class SectionMappingService
{
    private readonly IGenericRepository<SectionMapping> _repository;

    public SectionMappingService(IGenericRepository<SectionMapping> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse> GetAllAsync()
    {
        var mappings = await _repository.Query()
            .Include(sm => sm.School)
            .Include(sm => sm.Class)
            .Include(sm => sm.Section)
            .Include(sm => sm.Medium)
            .Include(sm => sm.User)
            .Select(sm => new SectionMappingDto
            {
                SectionMappingId = sm.SectionMappingId,
                SchoolId = sm.SchoolId,
                SchoolName = sm.School.SchoolName,
                ClassId = sm.ClassId,
                ClassName = sm.Class.ClassName,
                SectionId = sm.SectionId,
                SectionName = sm.Section.SectionName,
                MediumId = sm.MediumId,
                MediumName = sm.Medium.MediumName,
                UserId = sm.UserId,
                UserName = sm.User.Username, // adjust property if different
                RoomNumber = sm.RoomNumber,
                StatusId = sm.StatusId,
                DisplayOrder = sm.DisplayOrder
            })
            .ToListAsync();

        return new ApiResponse(true, "Section mappings retrieved successfully", mappings);
    }

    public async Task<ApiResponse> GetByIdAsync(int id)
    {
        var sm = await _repository.Query()
            .Include(sm => sm.School)
            .Include(sm => sm.Class)
            .Include(sm => sm.Section)
            .Include(sm => sm.Medium)
            .Include(sm => sm.User)
            .Where(sm => sm.SectionMappingId == id)
            .Select(sm => new SectionMappingDto
            {
                SectionMappingId = sm.SectionMappingId,
                SchoolId = sm.SchoolId,
                SchoolName = sm.School.SchoolName,
                ClassId = sm.ClassId,
                ClassName = sm.Class.ClassName,
                SectionId = sm.SectionId,
                SectionName = sm.Section.SectionName,
                MediumId = sm.MediumId,
                MediumName = sm.Medium.MediumName,
                UserId = sm.UserId,
                UserName = sm.User.Username,
                RoomNumber = sm.RoomNumber,
                StatusId = sm.StatusId,
                DisplayOrder = sm.DisplayOrder
            })
            .FirstOrDefaultAsync();

        if (sm == null)
            return new ApiResponse(false, "Section mapping not found");

        return new ApiResponse(true, "Section mapping retrieved successfully", sm);
    }

    public async Task<ApiResponse> AddAsync(SectionMappingDto dto)
    {
        var sm = new SectionMapping
        {
            SchoolId = dto.SchoolId,
            ClassId = dto.ClassId,
            SectionId = dto.SectionId,
            MediumId = dto.MediumId,
            UserId = dto.UserId,
            RoomNumber = dto.RoomNumber,
            StatusId = dto.StatusId,
            DisplayOrder = dto.DisplayOrder,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = 1 // Replace with logged-in user
        };

        await _repository.AddAsync(sm);
        await _repository.SaveChangesAsync();

        return new ApiResponse(true, "Section mapping added successfully", sm);
    }

    public async Task<ApiResponse> UpdateAsync(SectionMappingDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.SectionMappingId);
        if (existing == null)
            return new ApiResponse(false, "Section mapping not found");

        _repository.Detach(existing);

        var sm = new SectionMapping
        {
            SectionMappingId = dto.SectionMappingId,
            SchoolId = dto.SchoolId,
            ClassId = dto.ClassId,
            SectionId = dto.SectionId,
            MediumId = dto.MediumId,
            UserId = dto.UserId,
            RoomNumber = dto.RoomNumber,
            StatusId = dto.StatusId,
            DisplayOrder = dto.DisplayOrder,
            CreatedOn = existing.CreatedOn,
            CreatedBy = existing.CreatedBy,
            UpdatedOn = DateTime.UtcNow,
            UpdatedBy = 1 // Replace with logged-in user
        };

        _repository.Update(sm);
        await _repository.SaveChangesAsync();

        return new ApiResponse(true, "Section mapping updated successfully", sm);
    }

    public async Task<ApiResponse> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return new ApiResponse(false, "Section mapping not found");

        _repository.Remove(existing);
        await _repository.SaveChangesAsync();

        return new ApiResponse(true, "Section mapping deleted successfully");
    }
}