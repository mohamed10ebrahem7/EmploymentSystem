namespace EmploymentSystem.Domain.Entities
{
    public class Employer
    {
        public int Id { get; set; } // PK
        public string IdentityUserId { get; set; } // Foreign Key to IdentityUser
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
