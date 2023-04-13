using System.ComponentModel.DataAnnotations;

namespace PivotTest.Models
{
    public class ResponseReportPivot
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public int EventYear { get; set; }

    }
}
