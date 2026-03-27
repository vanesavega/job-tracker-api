using System.ComponentModel.DataAnnotations;

namespace JobTrackerApi.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        public ApplicationStatus Status { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public string? Url { get; set; }

        public string? SalaryRange { get; set; }

        public Company? Company { get; set; }
    }
}