namespace Application.DTOs
{
    public class CourseCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
    }

    public class CourseUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
    }

    public class CourseReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
    }
}