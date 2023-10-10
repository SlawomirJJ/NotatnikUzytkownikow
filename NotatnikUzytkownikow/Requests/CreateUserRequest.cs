using NotatnikUzytkownikow.Entities;
using NotatnikUzytkownikow.Enums;
using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Requests
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public Genders Gender { get; set; }

        public List<AdditionalAttributeRequest>? AdditionalAttributes { get; set; }

    }

    
}
