using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentReturnDto>> GetAllAsync()
        {
            var departments = await _context.Departments.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentReturnDto>>(departments);
        }

        public async Task<DepartmentReturnDto?> GetByIdAsync(int id)
        {
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            return department == null ? null : _mapper.Map<DepartmentReturnDto>(department);
        }

        public async Task<DepartmentReturnDto> CreateAsync(DepartmentCreateDto dto)
        {
            var entity = _mapper.Map<Department>(dto);
            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentReturnDto>(entity);
        }

        public async Task<bool> UpdateAsync(DepartmentUpdateDto dto)
        {
            var entity = await _context.Departments.FindAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity == null) return false;

            _context.Departments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}