using Application.DTOs;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorReturnDto>> GetAllAsync();
        Task<DoctorReturnDto?> GetByIdAsync(int id);
        Task<DoctorReturnDto> CreateAsync(DoctorCreateDto dto);
        Task<bool> UpdateAsync(DoctorUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}