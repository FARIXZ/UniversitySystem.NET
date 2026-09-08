using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnrollmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentReturnDto>> GetAllAsync()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<EnrollmentReturnDto>>(enrollments);
        }

        public async Task<EnrollmentReturnDto?> GetByIdAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            return enrollment == null ? null : _mapper.Map<EnrollmentReturnDto>(enrollment);
        }

        public async Task<EnrollmentReturnDto> CreateAsync(EnrollmentCreateDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            _context.Enrollments.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(e => e.Student).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Course).LoadAsync();
            return _mapper.Map<EnrollmentReturnDto>(entity);
        }

        public async Task<bool> UpdateAsync(EnrollmentUpdateDto dto)
        {
            var entity = await _context.Enrollments.FindAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Enrollments.FindAsync(id);
            if (entity == null) return false;

            _context.Enrollments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}