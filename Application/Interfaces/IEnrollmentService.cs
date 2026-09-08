using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentReturnDto>> GetAllAsync();
        Task<EnrollmentReturnDto?> GetByIdAsync(int id);
        Task<EnrollmentReturnDto> CreateAsync(EnrollmentCreateDto dto);
        Task<bool> UpdateAsync(EnrollmentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}