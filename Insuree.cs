using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Insuree
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int CarYear { get; set; }

        [Required]
        public string CarMake { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public int SpeedingTickets { get; set; }

        [Required]
        public bool HasDUI { get; set; }

        [Required]
        public string CoverageType { get; set; }

        public decimal Quote { get; set; }
    }
}
