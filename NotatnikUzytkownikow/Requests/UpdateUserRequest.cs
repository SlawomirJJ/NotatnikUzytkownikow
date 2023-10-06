using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }

        public List<AdditionalAttributeRequest>? AdditionalAttributes { get; set; }
    }
}
