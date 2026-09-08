using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorReturnDto>> GetAllAsync()
        {
            var doctors = await _context.Doctors.Include(d => d.Department).AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<DoctorReturnDto>>(doctors);
        }

        public async Task<DoctorReturnDto?> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Department).AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
            return doctor == null ? null : _mapper.Map<DoctorReturnDto>(doctor);
        }

        public async Task<DoctorReturnDto> CreateAsync(DoctorCreateDto dto)
        {
            var entity = _mapper.Map<Doctor>(dto);
            _context.Doctors.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(d => d.Department).LoadAsync();
            return _mapper.Map<DoctorReturnDto>(entity);
        }

        public async Task<bool> UpdateAsync(DoctorUpdateDto dto)
        {
            var entity = await _context.Doctors.FindAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Doctors.FindAsync(id);
            if (entity == null) return false;

            _context.Doctors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}