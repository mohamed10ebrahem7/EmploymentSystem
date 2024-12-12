namespace EmploymentSystem.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public DateTime AppliedDate { get; set; }

        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
