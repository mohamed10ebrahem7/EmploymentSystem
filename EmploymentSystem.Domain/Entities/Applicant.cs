namespace EmploymentSystem.Domain.Entities
{
    public class Applicant
    {
        public int Id { get; set; } // PK
        public string IdentityUserId { get; set; } // Foreign Key to IdentityUser
        public ICollection<Application> Applications { get; set; }
    }
}
