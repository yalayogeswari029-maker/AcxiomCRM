using System.ComponentModel.DataAnnotations;

namespace AcxiomCRM.Models
{
    public class FollowUp
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime FollowUpDate { get; set; }

        public string? Notes { get; set; }

        public string Status { get; set; } = "Pending";
    }
}