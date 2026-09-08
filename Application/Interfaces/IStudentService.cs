using Application.DTOs;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentReturnDto>> GetAllAsync();
        Task<StudentReturnDto?> GetByIdAsync(int id);
        Task<StudentReturnDto> CreateAsync(StudentCreateDto dto);
        Task<bool> UpdateAsync(StudentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}