using NotatnikUzytkownikow.Entities;
using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Requests
{
    public class CreateUserRequest
    {
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

    public class AdditionalAttributeRequest
    {
        [Required]
        public string AttributeName { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
