using Application.DTOs;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentReturnDto>> GetAllAsync();
        Task<DepartmentReturnDto?> GetByIdAsync(int id);
        Task<DepartmentReturnDto> CreateAsync(DepartmentCreateDto dto);
        Task<bool> UpdateAsync(DepartmentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}