namespace Application.DTOs
{
    public class EnrollmentCreateDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Grade { get; set; } = string.Empty;
    }

    public class EnrollmentUpdateDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Grade { get; set; } = string.Empty;
    }

    public class EnrollmentReturnDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Grade { get; set; } = string.Empty;
    }
}