using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class CustomerEntity
    {
        [Key] public int Id { get; set; }
        [Required] public string? FirstName { get; set; }
        [Required] public string? LastName { get; set; }
        [Required]  public string? Email { get; set; }
        [Required]  public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
