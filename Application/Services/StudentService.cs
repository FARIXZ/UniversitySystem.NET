using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentReturnDto>> GetAllAsync()
        {
            var students = await _context.Students.Include(s => s.Department).AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<StudentReturnDto>>(students);
        }

        public async Task<StudentReturnDto?> GetByIdAsync(int id)
        {
            var student = await _context.Students.Include(s => s.Department).AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            return student == null ? null : _mapper.Map<StudentReturnDto>(student);
        }

        public async Task<StudentReturnDto> CreateAsync(StudentCreateDto dto)
        {
            var entity = _mapper.Map<Student>(dto);
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(s => s.Department).LoadAsync();
            return _mapper.Map<StudentReturnDto>(entity);
        }

        public async Task<bool> UpdateAsync(StudentUpdateDto dto)
        {
            var entity = await _context.Students.FindAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Students.FindAsync(id);
            if (entity == null) return false;

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}