namespace Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // Many-to-many self-reference via bridging entity CoursePrerequisite
        public ICollection<CoursePrerequisite> Prerequisites { get; set; } = new List<CoursePrerequisite>();
        public ICollection<CoursePrerequisite> IsPrerequisiteFor { get; set; } = new List<CoursePrerequisite>();
    }
}