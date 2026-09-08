using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CourseService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseReturnDto>> GetAllAsync()
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Doctor)
                .AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<CourseReturnDto>>(courses);
        }

        public async Task<CourseReturnDto?> GetByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Doctor)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            return course == null ? null : _mapper.Map<CourseReturnDto>(course);
        }

        public async Task<CourseReturnDto> CreateAsync(CourseCreateDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(c => c.Department).LoadAsync();
            await _context.Entry(entity).Reference(c => c.Doctor).LoadAsync();
            return _mapper.Map<CourseReturnDto>(entity);
        }

        public async Task<bool> UpdateAsync(CourseUpdateDto dto)
        {
            var entity = await _context.Courses.FindAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Courses.FindAsync(id);
            if (entity == null) return false;

            _context.Courses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}