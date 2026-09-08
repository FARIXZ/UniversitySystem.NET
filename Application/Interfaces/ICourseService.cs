using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseReturnDto>> GetAllAsync();
        Task<CourseReturnDto?> GetByIdAsync(int id);
        Task<CourseReturnDto> CreateAsync(CourseCreateDto dto);
        Task<bool> UpdateAsync(CourseUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}