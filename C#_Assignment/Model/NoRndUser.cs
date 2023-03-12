using System.ComponentModel.DataAnnotations;

namespace C__Assignment.Model
{
    public class NoRndUser
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Country { get; set; }
    }
}
