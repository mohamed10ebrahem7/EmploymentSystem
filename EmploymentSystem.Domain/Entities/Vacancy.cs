﻿namespace EmploymentSystem.Domain.Entities
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public int EmployerId { get; set; }
        public Employer Employer { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
