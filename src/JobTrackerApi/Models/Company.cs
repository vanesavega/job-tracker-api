using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobTrackerApi.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Website { get; set; }

        public string? Industry { get; set; }

        [JsonIgnore]
        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}